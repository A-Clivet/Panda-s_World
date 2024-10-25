using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class VillageGenerator : MonoBehaviour
{
    public BiomeTilemapGenerator biomeTileMapGenerator; // Reference to the BiomeTileMapGenerator
    public Transform CameraBody; // Reference to the Camera body
    public int radius = 10;
    
    
    private Vector2Int villageCenter;

    private void Awake()
    {
        SetRandomVillageCenter();
    }

    private void Start()
    {
        // S'abonner à l'événement OnChunksGenerated
        biomeTileMapGenerator.OnChunksGenerated += StartVillageGenerator;
    }
    
    public void StartVillageGenerator()
    {
        if (IsVillageCenterValid())
        {
            Debug.Log("Village center is valid.");
        }
        else
        {
            Debug.Log("Village center is invalid. It is too close to the sea.");
            SetRandomVillageCenter();
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
                 Biome biome = biomeTileMapGenerator.GetBiomeAtPosition(tilePos);
                 if (biome.name == "Mer")
                 {
                     return false;
                 }
             }
         }
         return true;
     }
}