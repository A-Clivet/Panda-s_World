using UnityEngine;
using UnityEngine.Tilemaps;

public class Chunk
{
    public int chunkX;
    public int chunkY;
    public Tilemap tilemap; // Tilemap associée à ce chunk
    public Biome[,] biomeData; // Stocke les données des biomes pour ce chunk

    public Chunk(int x, int y, int width, int height)
    {
        chunkX = x;
        chunkY = y;
        biomeData = new Biome[width, height];
    }
}