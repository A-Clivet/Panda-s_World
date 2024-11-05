using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaGenerator : MonoBehaviour
{
    public VillageGenerator villageGenerator;
    public GameObject pandaPrefab;
    public int startPandaCount = 10;
    
    private void Awake()
    {
        villageGenerator.VillageIsBuilt += PandaSpawn;
    }

    public void PandaSpawn(Vector2Int villageCenter)
    {
        for (int i = 0; i < startPandaCount; i++)
        {
            GameObject panda =  Instantiate (pandaPrefab, new Vector3(villageCenter.x,villageCenter.y, -1), Quaternion.identity, transform);
            villageGenerator.biomeTileMapGenerator.unitsTransform.Add(panda.transform);
        }
    }
}
