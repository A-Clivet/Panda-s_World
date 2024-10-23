using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageGenerator : MonoBehaviour
{
    public BiomeTilemapGenerator biomeTileMapGenerator; // Reference to the BiomeTileMapGenerator
    private Vector2Int villageCenter;
    public int radius = 10;

    // Start is called before the first frame update
    void Start()
    {
        SetRandomVillageCenter();
        Debug.Log($"Village center is at: {villageCenter}");
        // if (IsVillageCenterValid())
        // {
        //     Debug.Log("Village center is valid.");
        // }
        // else
        // {
        //     Debug.Log("Village center is invalid. It is too close to the sea.");
        // }
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

//     private bool IsVillageCenterValid()
//     {
//         for (int y = -radius; y <= radius; y++)
//         {
//             for (int x = -radius; x <= radius; x++)
//             {
//                 Vector2Int tilePos = new Vector2Int(villageCenter.x + x, villageCenter.y + y);
//                 Biome biome = biomeTileMapGenerator.GetBiomeAtPosition(tilePos);
//                 if (biome.name == "Sea")
//                 {
//                     return false;
//                 }
//             }
//         }
//         return true;
//     }
}