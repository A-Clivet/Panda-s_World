using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public List<Vector2Int> DiamondPos = new List<Vector2Int>();
    public List<Vector2Int> WoodPos = new List<Vector2Int>();
    public List<Vector2Int> RockPos = new List<Vector2Int>();
    public TilemapGenerator tileMapGenerator;

    private void Awake()
    {
        tileMapGenerator.NewResourceGenerated += AddResource;
    }

    public void AddResource(Resource resource, Vector2Int position)
    {
        switch(resource.name)
        {
            case "Diamant":
                DiamondPos.Add(position);
                break;
            case "Bois":
                WoodPos.Add(position);
                break;
            case "Pierre":
                RockPos.Add(position);
                break;
        }
    }
}