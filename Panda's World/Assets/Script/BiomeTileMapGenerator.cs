using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using Random = Unity.Mathematics.Random;

[ExecuteInEditMode]
public class BiomeTilemapGenerator : MonoBehaviour
{
    [Header("Tilemap Settings")]
    public Tilemap tilemap; // Assignez la Tilemap ici

    [Header("Map Settings")]
    public int mapWidth = 100;
    public int mapHeight = 100;
    [Range(1f, 100f)]
    public float noiseScale = 20f; // Contrôle la taille des zones

    [Header("Perlin Noise Offsets")]
    public float xOffset = 0f;
    public float yOffset = 0f;

    [Header("Biomes")]
    public Biome[] biomes; // Assigné via l'inspecteur

    [FormerlySerializedAs("lakeThreshold")]
    [Header("More Environment Settings")]
    [Range(0f, 1f)]
    public float environmentThreshold = 0.05f; // Probabilité d'avoir un un environement interne
    public float environmentNoiseScale = 100f; // Échelle du bruit pour les environements interne
    
    [Header("Tile Assignments")]
    public TileBase defaultTile; // Utilisé si aucun biome n'est assigné

    private void Start()
    {
        GenerateTilemap();
    }

#if UNITY_EDITOR
    // private void Update()
    // {
    //     if (!Application.isPlaying)
    //     {
    //         GenerateTilemap();
    //     }
    // }

    [ContextMenu("Regenerate Tilemap")]
    public void RegenerateTilemap()
    {
        GenerateTilemap();
        UnityEditor.EditorUtility.SetDirty(this);
    }
#endif

    public void GenerateTilemap()
    {
        if (tilemap == null)
        {
            Debug.LogError("Tilemap n'est pas assignée !");
            return;
        }

        tilemap.ClearAllTiles();

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float xCoord = (float)x / mapWidth * noiseScale + xOffset;
                float yCoord = (float)y / mapHeight * noiseScale + yOffset;

                float perlinValue = Mathf.PerlinNoise(xCoord, yCoord);
                Biome assignedBiome = GetBiome(perlinValue);

            // Vérifie si le biome possede un environement avancé
                // if (assignedBiome.environment && assignedBiome.environmentRuleTile != null)
                // {
                //     float environmentSampleX = (x / (float)mapWidth) * environmentNoiseScale + xOffset;
                //     float environmentSampleY = (y / (float)mapHeight) * environmentNoiseScale + yOffset;
                //     float environmentNoise = Mathf.PerlinNoise(environmentSampleX, environmentSampleY);
                //
                //     int spawnRate = new System.Random().Next(0, 100);
                //     if (assignedBiome.terrainVariation  >= spawnRate && environmentNoise < environmentThreshold)
                //     {
                //         Debug.Log(spawnRate);
                //         tilemap.SetTile(new Vector3Int(x, y, 0), assignedBiome.environmentRuleTile);
                //         continue;
                //     }
                // }
                //
                // Assigner le Rule Tile correspondant au biome
                RuleTile ruleTileToSet = GetRuleTileForBiome(assignedBiome);
                if (ruleTileToSet != null)
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), ruleTileToSet);
                }
                else
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), defaultTile);
                }
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
}
