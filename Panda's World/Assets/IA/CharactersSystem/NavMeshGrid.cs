// using System.Collections.Generic;
// using UnityEngine.Tilemaps;
// using UnityEngine;
//
// public class NavMeshGrid : MonoBehaviour
// {
//     public BiomeTilemapGenerator biomeTilemapGenerator;
//
//     private int width, height;
//     private int tileSize = 256;
//     private Node[,] grid;
//
//     private void OnEnable()
//     {
//         biomeTilemapGenerator.OnchunkGenerated += GenerateNavMeshGrid;
//     }
//
//     private void OnDisable()
//     {
//         biomeTilemapGenerator.OnchunkGenerated -= GenerateNavMeshGrid;
//     }
//
//     private void GenerateNavMeshGrid()
//     {
//         width = biomeTilemapGenerator.chunkWidth;
//         height = biomeTilemapGenerator.chunkHeight;
//         
//         grid = new Node[width, height];
//
//         for (int x = 0; x < width; x++)
//         {
//             for (int y = 0; y < height; y++)
//             {
//                 Vector2Int worldPosition = new Vector2Int(x, y);
//                 TileBase tile = biomeTilemapGenerator.GetTileAtPosition(worldPosition);
//                 
//                 bool walkable = tile&& IsTileWalkable(tile);
//                 grid[x, y] = new Node(x, y, walkable);
//             }
//         }
//
//         Debug.Log("NavMeshGrid généré !");
//     }
//
//     private bool IsTileWalkable(TileBase tile)
//     {
//         if (tile.name != "TuileDeMer")
//         {
//             return true;
//         }
//         return false;
//     }
//     
//     public List<Node> GetNeighbors(Node node)
//     {
//         List<Node> neighbors = new List<Node>();
//
//         for (int x = -1; x <= 1; x++)
//         {
//             for (int y = -1; y <= 1; y++)
//             {
//                 if (x == 0 && y == 0) continue; // Ignore le node actuel
//
//                 int checkX = node.x + x;
//                 int checkY = node.y + y;
//
//                 if (checkX >= 0 && checkX < width && checkY >= 0 && checkY < height)
//                 {
//                     neighbors.Add(grid[checkX, checkY]);
//                 }
//             }
//         }
//
//         return neighbors;
//     }
//     
//     public Node[,] GetGrid()
//     {
//         return grid;
//     }
//     
//     void OnDrawGizmos()
//     {
//         if (grid != null)
//         {
//             foreach (Node node in grid)
//             {
//                 Gizmos.color = node.walkable ? Color.green : Color.red;
//                 Gizmos.DrawCube(new Vector3(node.x * tileSize, node.y * tileSize, 0), Vector3.one * (tileSize - 0.1f));
//             }
//         }
//     }
//
// }
//
// public class Node
// {
//     public int x;
//     public int y;
//     public bool walkable;
//
//     // Coûts utilisés dans A*
//     public int gCost; // Coût du début jusqu'à ce node
//     public int hCost; // Estimation du coût du node jusqu'à la cible
//     public int fCost => gCost + hCost; // Coût total (gCost + hCost)
//     public Node parent; // Référence au node parent pour retracer le chemin
//
//     public Node(int x, int y, bool walkable)
//     {
//         this.x = x;
//         this.y = y;
//         this.walkable = walkable;
//
//         // Initialiser les coûts à un nombre élevé par défaut
//         gCost = int.MaxValue;
//         hCost = 0;
//         parent = null;
//     }
// }
