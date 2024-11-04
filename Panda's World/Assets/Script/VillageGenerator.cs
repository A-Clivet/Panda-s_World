using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class VillageGenerator : MonoBehaviour
{
    public BiomeTilemapGenerator biomeTileMapGenerator; // Reference to the BiomeTileMapGenerator
    public BuildingGenerations buildingGenerations;
    public Transform CameraBody; // Reference to the Camera body
    public int radius = 10;
    public RuleTile roadTile;
    
    
    private Vector2Int villageCenter;

    private void Awake()
    {
        SetRandomVillageCenter();
        biomeTileMapGenerator.OnChunksGenerated += StartVillageGenerator;
    }


    
    public void StartVillageGenerator()
    {
        if (IsVillageCenterValid())
        {
            Debug.Log("Village center is valid.");
            VillageConstruction();
            // buildingGenerations.BuildingCreations(villageCenter);
            
        }
        else
        {
            Debug.Log("Village center is invalid. It is too close to the sea.");
            SetRandomVillageCenter();
            biomeTileMapGenerator.TryGenerateChunks(); // trouver une solution plus propre (via l'injection de dependances)
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
                 TileBase biome = biomeTileMapGenerator.GetTileAtPosition(tilePos);
                 if (biome.name == "TileDeMer")
                 {
                     return false;
                 }
             }
         }
         return true;
     }
     
     private void VillageConstruction()
     {
         List<Vector2Int> villageTiles = new List<Vector2Int>();
         for (int y = -radius; y <= radius; y++)
         {
             for (int x = -radius; x <= radius; x++)
             {
                 Vector2Int tilePos = new Vector2Int(villageCenter.x + x, villageCenter.y + y);
                    villageTiles.Add(tilePos);
                 biomeTileMapGenerator.SetTileAtPosition(tilePos, roadTile);
             }
         }
         
         // TODO : Mettre a jour les Visuels des Tiles (toutes)
         for (int i = 0; i < 5; i=i)
         {
             Vector2Int housePosition = villageTiles[Random.Range(0, villageTiles.Count)];
             if (buildingGenerations.BuildingCreations(housePosition))
             {
                 i+=1;
             }
         }
     }
}