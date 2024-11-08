using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class BuildingGenerations : MonoBehaviour
{
    [FormerlySerializedAs("biomeTileMapGenerator")] public TilemapGenerator tileMapGenerator; // Reference to the BiomeTileMapGenerator
    public Batiment maison;
    public Batiment Route;
    public List<string> nonBuildableTiles;

    public bool BuildingCreations(Vector2Int position, Batiment batiment = null)
    {
        batiment = maison;
        // check si il y a la place pour construire le batiment
        // verifie les tiles autour de la taille centrale du batiment selon la taille du batiment
        for (int y = -batiment.sizeY / 2; y <= batiment.sizeY / 2 + 1; y++)
        {
            for (int x = -batiment.sizeX / 2; x <= batiment.sizeX / 2 + 1; x++)
            {
                Vector2Int tilePos = new Vector2Int(position.x + x, position.y + y);
                TileBase tileName = tileMapGenerator.GetTileAtPosition(tilePos);

                foreach (var name in nonBuildableTiles) 
                {
                    if (tileName.name == name)
                    {
                        return false;
                    }
                }
            }
        }
        for (int y = -batiment.sizeY / 2; y <= batiment.sizeY / 2; y++)
        {
            for (int x = -batiment.sizeX / 2; x <= batiment.sizeX / 2; x++)
            {
                Vector2Int tilePos = new Vector2Int(position.x + x, position.y + y);
                tileMapGenerator.SetTileAtPosition(tilePos, batiment.ruleTile);
                if (y == -batiment.sizeY / 2)
                {
                    tileMapGenerator.SetTileAtPosition(new Vector2Int(position.x + x, position.y + y - 1 ), Route.ruleTile);
                }
                if (x == -batiment.sizeX / 2)
                {
                    tileMapGenerator.SetTileAtPosition(new Vector2Int(position.x + x - 1, position.y + y), Route.ruleTile);
                }
                if (y == batiment.sizeY/2)
                {
                    tileMapGenerator.SetTileAtPosition(new Vector2Int(position.x + x, position.y + y + 1), Route.ruleTile);
                }
                if (x == batiment.sizeX/2)
                {
                    tileMapGenerator.SetTileAtPosition(new Vector2Int(position.x + x + 1, position.y + y), Route.ruleTile);
                }
               
            }
        }
        return true;
    }
}