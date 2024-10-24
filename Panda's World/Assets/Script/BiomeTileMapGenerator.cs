using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using Random = Unity.Mathematics.Random;

//[ExecuteInEditMode]
public class BiomeTilemapGenerator : MonoBehaviour
{
    [Header("Tilemap Settings")] public Tilemap tilemapPrefab; // Prefab de la Tilemap pour chaque chunk
    public Grid Grid; // Prefab de la Tilemap pour chaque chunk

    [Header("Chunk Settings")] public int chunkWidth = 16;
    public int chunkHeight = 16;

    private Dictionary<Vector2Int, Chunk> chunks = new Dictionary<Vector2Int, Chunk>();

    [FormerlySerializedAs("unitTransform")] [Header("Unit Settings")]
    public List<Transform> unitsTransform; // Assigné dans l'éditeur au unité

    [Header("Chunk Settings")]
    public int viewDistanceInChunks = 4; // Nombre de chunks à charger autour du joueur/caméra

    [Header("Map Settings")] public int mapWidth = 100;
    public int mapHeight = 100;
    [Range(1f, 100f)] public float noiseScale = 20f; // Contrôle la taille des zones

    [Header("Perlin Noise Offsets")] public float xOffset = 0f;
    public float yOffset = 0f;

    [Header("Biomes")] public Biome[] biomes; // Assigné via l'inspecteur

    [FormerlySerializedAs("lakeThreshold")] [Header("More Environment Settings")] [Range(0f, 1f)]
    public float environmentThreshold = 0.05f; // Probabilité d'avoir un un environement interne

    public float environmentNoiseScale = 100f; // Échelle du bruit pour les environements interne

    [Header("Tile Assignments")] public TileBase defaultTile; // Utilisé si aucun biome n'est assigné


    // Private variables
    private Vector2Int currentPlayerChunkPos;

    private void Update()
    {
        List<Vector2Int> playerChunkPositions = new List<Vector2Int>();

        foreach (var unit in unitsTransform)
        {
            // Recuper la position (du chunk) de l'unité
            Vector2Int playerChunkPos = new Vector2Int(
                Mathf.FloorToInt(unit.position.x / chunkWidth),
                Mathf.FloorToInt(unit.position.y / chunkHeight)
            );

            // ajouter la position du chunk de l'unité à la liste
            playerChunkPositions.Add(playerChunkPos);
        }

        // Mise à jour des chunks en fonction de la liste des positions des chunks des unté
        UpdateChunks(playerChunkPositions);
    }

    private void UpdateChunks(List<Vector2Int> playerChunkPositions)
    {
        // Calculer la surface visible en chunks
        List<Vector2Int> chunksToLoad = new List<Vector2Int>();

        foreach (var playerChunkPos in playerChunkPositions)
        {
            for (int y = -viewDistanceInChunks; y <= viewDistanceInChunks; y++)
            {
                for (int x = -viewDistanceInChunks; x <= viewDistanceInChunks; x++)
                {
                    Vector2Int chunkPos = new Vector2Int(
                        playerChunkPos.x + x,
                        playerChunkPos.y + y
                    );
                    chunksToLoad.Add(chunkPos);

                    // Si le chunk n'existe pas encore, il est créé
                    if (!chunks.ContainsKey(chunkPos))
                    {
                        CreateChunk(chunkPos);
                    }
                    else
                    {
                        // Réactive le chunk s'il existe déjà
                        chunks[chunkPos].tilemap.gameObject.transform.parent.gameObject.SetActive(true);
                    }
                }
            }
        }

        // Décharger les chunks qui ne sont plus dans la zone visible
        List<Vector2Int> chunksToUnload = new List<Vector2Int>();
        foreach (Vector2Int loadedChunk in chunks.Keys)
        {
            if (!chunksToLoad.Contains(loadedChunk))
            {
                chunksToUnload.Add(loadedChunk);
            }
        }

        foreach (Vector2Int chunkPos in chunksToUnload)
        {
            UnloadChunk(chunkPos);
        }
    }

    private void UnloadChunk(Vector2Int chunkPos)
    {
        // Désactiver le GameObject du chunk
        if (chunks.ContainsKey(chunkPos))
        {
            Chunk chunk = chunks[chunkPos];
            chunk.tilemap.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }

    public void GenerateChunks()
    {
        for (int y = 0; y < mapHeight; y += chunkHeight)
        {
            for (int x = 0; x < mapWidth; x += chunkWidth)
            {
                Vector2Int chunkPos = new Vector2Int(x / chunkWidth, y / chunkHeight);
                CreateChunk(chunkPos);
            }
        }
    }

    private void CreateChunk(Vector2Int chunkPos)
    {
        // Créer un GameObject pour ce chunk
        GameObject chunkGO = new GameObject($"Chunk_{chunkPos.x}_{chunkPos.y}");

        // Assigner la position globale dans le monde pour ce chunk (en fonction de sa taille)
        chunkGO.transform.position = new Vector3(chunkPos.x * chunkWidth, chunkPos.y * chunkHeight, 0);

        // Définir Grid comme parent de chunkGO
        chunkGO.transform.SetParent(Grid.transform);

        // Instancier la Tilemap à partir du prefab et l'attacher au chunk
        Tilemap chunkTilemap = Instantiate(tilemapPrefab, chunkGO.transform);
        chunkTilemap.name = $"Tilemap_{chunkPos.x}_{chunkPos.y}";

        // Créer un Chunk et assigner la Tilemap associée
        Chunk newChunk = new Chunk(chunkPos.x, chunkPos.y, chunkWidth, chunkHeight);
        newChunk.tilemap = chunkTilemap;

        // Ajouter le chunk à la liste des chunks
        chunks.Add(chunkPos, newChunk);

        // Générer les tiles pour ce chunk
        GenerateTilesForChunk(newChunk);
    }


    private void GenerateTilesForChunk(Chunk chunk)
    {
        for (int y = 0; y < chunkHeight; y++)
        {
            for (int x = 0; x < chunkWidth; x++)
            {
                int worldX = chunk.chunkX * chunkWidth + x;
                int worldY = chunk.chunkY * chunkHeight + y;

                float xCoord = (float)worldX / mapWidth * noiseScale + xOffset;
                float yCoord = (float)worldY / mapHeight * noiseScale + yOffset;

                float perlinValue = Mathf.PerlinNoise(xCoord, yCoord);
                Biome assignedBiome = GetBiome(perlinValue);

                // Générer la tuile du biome
                RuleTile ruleTileToSet = GetRuleTileForBiome(assignedBiome);
                if (ruleTileToSet != null)
                {
                    chunk.tilemap.SetTile(new Vector3Int(x, y, 0), ruleTileToSet);
                }
                else
                {
                    chunk.tilemap.SetTile(new Vector3Int(x, y, 0), defaultTile);
                }

                // Génération des ressources dans ce biome
                TryGenerateRessource(assignedBiome, chunk.tilemap, x, y);
            }
        }
    }

    private void TryGenerateRessource(Biome biome, Tilemap tilemap, int x, int y)
    {
        foreach (Ressource ressource in biome.ressources)
        {
            // Générer un nombre aléatoire pour savoir si on place cette ressource
            int spawnRate = UnityEngine.Random.Range(0, 101);
            
            if (ressource.rarity > spawnRate) // Si la rareté est supérieure au spawn rate, on place la ressource
            {
                // tiles spécifiques pour chaque ressource
                tilemap.SetTile(new Vector3Int(x, y, 0), ressource.ruleTile);
            }
        }
    }


    private Biome GetBiome(float noiseValue)
    {
        foreach (Biome biome in biomes)
        {
            if (noiseValue < biome.threshold)
            {
                return biome;
            }
        }

        return biomes[biomes.Length - 1]; // Retourner le dernier biome si aucune condition n'est remplie
    }

    private RuleTile GetRuleTileForBiome(Biome biome)
    {
        return biome.ruleTile;
    }
    
    public Biome GetBiomeAtPosition(Vector2Int position)
    {
        int chunkX = position.x / chunkWidth;
        int chunkY = position.y / chunkHeight;
        Vector2Int chunkPos = new Vector2Int(chunkX, chunkY);
        Debug.Log(chunkPos);

        if (chunks.ContainsKey(chunkPos))
        {
            Debug.Log("ok");
            Chunk chunk = chunks[chunkPos];
            Debug.Log(chunk);
            int localX = position.x % chunkWidth;
            int localY = position.y % chunkHeight;

            float xCoord = (float)position.x / mapWidth * noiseScale + xOffset;
            float yCoord = (float)position.y / mapHeight * noiseScale + yOffset;

            float perlinValue = Mathf.PerlinNoise(xCoord, yCoord);
            return GetBiome(perlinValue);
        }

        Debug.Log("echec");
        return null; // Return null if the chunk is not loaded
    }
}