using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class VillageGenerator : MonoBehaviour
{
    public event Action<Vector2Int> VillageIsBuilt;

    [FormerlySerializedAs("biomeTileMapGenerator")] public TilemapGenerator tileMapGenerator; // Reference to the BiomeTileMapGenerator
    public BuildingGenerations buildingGenerations;
    public Transform CameraBody; // Reference to the Camera body
    public int radius = 10;
    public Batiment road;


    private Vector2Int villageCenter;

    private void Awake()
    {
        SetRandomVillageCenter();
        tileMapGenerator.SpawnVillage += StartVillageGenerator;
    }


    public void StartVillageGenerator()
    {
        if (IsVillageCenterValid())
        {
            VillageConstruction();
            tileMapGenerator.SpawnVillage -= StartVillageGenerator;

        }
        else
        {
            SetRandomVillageCenter();
            tileMapGenerator.TryGenerateChunks(); // NEED : trouver une solution plus propre (via l'injection de dependances)
            StartVillageGenerator();
        }
    }

    private void SetRandomVillageCenter()
    {
        int x = Random.Range(3000, 13000);
        int y = Random.Range(3000, 13000);
        villageCenter = new Vector2Int(x, y);
        CameraBody.transform.position = new Vector3(villageCenter.x, villageCenter.y, -10);
    }

    private bool IsVillageCenterValid()
    {
        for (int y = -radius; y <= radius; y++)
        {
            for (int x = -radius; x <= radius; x++)
            {
                Vector2Int tilePos = new Vector2Int(villageCenter.x + x, villageCenter.y + y);
                TileBase biome = tileMapGenerator.GetTileAtPosition(tilePos);
                if (biome.name == "TileDeMer")
                {
                    return false;
                }
            }
        }

        return true;
    }

    private void VillageConstruction() // TODO: faire en sorte que les tiles bâtiments remplacent les tiles de biome et possèdent leur propre poids A*
    {
        List<Vector2Int> villageTiles = new List<Vector2Int>();
        for (int y = -radius; y <= radius; y++)
        {
            for (int x = -radius; x <= radius; x++)
            {
                Vector2Int tilePos = new Vector2Int(villageCenter.x + x, villageCenter.y + y);
                villageTiles.Add(tilePos);
                tileMapGenerator.SetTileAtPosition(tilePos, road);
            }
        }
        
        for (int i = 0; i < 5; i++)
        {
            Vector2Int housePosition = villageTiles[Random.Range(0, villageTiles.Count)];
            buildingGenerations.BuildingCreations(housePosition);
        }
        
        VillageIsBuilt?.Invoke(villageCenter);
    }
}