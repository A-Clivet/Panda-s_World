// using System.Collections.Generic;
// using UnityEngine;
//
// public class Pathfinding : MonoBehaviour
// {
//     public NavMeshGrid navMeshGrid;
//     private Node[,] grid;
//
//     private void Start()
//     {
//         // Initialise la grille
//         grid = navMeshGrid.GetGrid();
//     }
//
//     public List<Node> FindPath(Vector2Int startPos, Vector2Int targetPos)
//     {
//         Node startNode = grid[startPos.x, startPos.y];
//         Node targetNode = grid[targetPos.x, targetPos.y];
//
//         List<Node> openSet = new List<Node> { startNode };
//         HashSet<Node> closedSet = new HashSet<Node>();
//
//         while (openSet.Count > 0)
//         {
//             Node currentNode = openSet[0];
//
//             // Trouver le noeud avec le coût total (fCost) le plus bas
//             for (int i = 1; i < openSet.Count; i++)
//             {
//                 if (openSet[i].fCost < currentNode.fCost || (openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost))
//                 {
//                     currentNode = openSet[i];
//                 }
//             }
//
//             openSet.Remove(currentNode);
//             closedSet.Add(currentNode);
//
//             // Si on a atteint la cible
//             if (currentNode == targetNode)
//             {
//                 return RetracePath(startNode, targetNode);
//             }
//
//             foreach (Node neighbor in navMeshGrid.GetNeighbors(currentNode))
//             {
//                 if (!neighbor.walkable || closedSet.Contains(neighbor)) continue;
//
//                 int newMovementCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);
//                 if (newMovementCostToNeighbor < neighbor.gCost || !openSet.Contains(neighbor))
//                 {
//                     neighbor.gCost = newMovementCostToNeighbor;
//                     neighbor.hCost = GetDistance(neighbor, targetNode);
//                     neighbor.parent = currentNode;
//
//                     if (!openSet.Contains(neighbor))
//                         openSet.Add(neighbor);
//                 }
//             }
//         }
//
//         return null; // Retourne null si aucun chemin n'est trouvé
//     }
//
//     private List<Node> RetracePath(Node startNode, Node endNode)
//     {
//         List<Node> path = new List<Node>();
//         Node currentNode = endNode;
//
//         while (currentNode != startNode)
//         {
//             path.Add(currentNode);
//             currentNode = currentNode.parent;
//         }
//         path.Reverse();
//         return path;
//     }
//
//     private int GetDistance(Node nodeA, Node nodeB)
//     {
//         int dstX = Mathf.Abs(nodeA.x - nodeB.x);
//         int dstY = Mathf.Abs(nodeA.y - nodeB.y);
//
//         return dstX > dstY ? 14 * dstY + 10 * (dstX - dstY) : 14 * dstX + 10 * (dstY - dstX);
//     }
// }
