using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageGenerator : MonoBehaviour
{
    public BiomeTilemapGenerator biomeTileMapGenerator; // Reference to the BiomeTileMapGenerator
    public Transform CameraBody; // Reference to the Camera body
    public int radius = 10;
    
    
    private Vector2Int villageCenter;


    // Start is called before the first frame update
    void Start()
    {
        SetRandomVillageCenter();
        CameraBody.transform.position = new Vector3(villageCenter.x, villageCenter.y, -10);
        
        if (IsVillageCenterValid())
        {
            Debug.Log("Village center is valid.");
        }
        else
        {
            Debug.Log("Village center is invalid. It is too close to the sea.");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetRandomVillageCenter()
    {
        int x = Random.Range(3000, 13000);
        int y = Random.Range(3000, 13000);
        villageCenter = new Vector2Int(x, y);
    }

     private bool IsVillageCenterValid()
     {
         for (int y = -radius; y <= radius; y++)
         {
             for (int x = -radius; x <= radius; x++)
             {
                 Vector2Int tilePos = new Vector2Int(villageCenter.x + x, villageCenter.y + y);
                 Biome biome = biomeTileMapGenerator.GetBiomeAtPosition(tilePos);
                 if (biome.biomeName == "Mer")
                 {
                     return false;
                 }
             }
         }
         return true;
     }
}