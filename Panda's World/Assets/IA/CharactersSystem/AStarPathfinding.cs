using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarPathfinding
{
    private Node[,] grid;

    public AStarPathfinding(Node[,] grid)
    {
        this.grid = grid;
    }

    public List<Vector2Int> FindPath(Vector2Int start, Vector2Int goal)
    {
        Node startNode = grid[start.x, start.y];
        Node goalNode = grid[goal.x, goal.y];

        List<Node> openList = new List<Node> { startNode };
        HashSet<Node> closedList = new HashSet<Node>();

        startNode.G = 0;
        startNode.H = GetHeuristic(start, goal);

        while (openList.Count > 0)
        {
            Node currentNode = GetNodeWithLowestF(openList);
            if (currentNode == goalNode)
                return RetracePath(startNode, goalNode);

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (Node neighbor in GetNeighbors(currentNode))
            {
                if (!neighbor.IsWalkable || closedList.Contains(neighbor))
                    continue;

                float newMovementCost = currentNode.G + GetDistance(currentNode, neighbor);
                if (newMovementCost < neighbor.G || !openList.Contains(neighbor))
                {
                    neighbor.G = newMovementCost;
                    neighbor.H = GetHeuristic(neighbor.Position, goal);
                    neighbor.Parent = currentNode;

                    if (!openList.Contains(neighbor))
                        openList.Add(neighbor);
                }
            }
        }

        return null; // Aucun chemin trouvé
    }

    private float GetHeuristic(Vector2Int a, Vector2Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }

    private Node GetNodeWithLowestF(List<Node> nodes)
    {
        Node lowestFNode = nodes[0];
        foreach (var node in nodes)
        {
            if (node.F < lowestFNode.F)
                lowestFNode = node;
        }
        return lowestFNode;
    }

    private List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();
        Vector2Int[] directions = {
            new Vector2Int(0, 1), new Vector2Int(1, 0),
            new Vector2Int(0, -1), new Vector2Int(-1, 0)
        };

        foreach (var dir in directions)
        {
            Vector2Int checkPos = node.Position + dir;
            if (IsPositionValid(checkPos))
                neighbors.Add(grid[checkPos.x, checkPos.y]);
        }

        return neighbors;
    }

    private bool IsPositionValid(Vector2Int position)
    {
        return position.x >= 0 && position.y >= 0 &&
               position.x < grid.GetLength(0) && position.y < grid.GetLength(1);
    }

    private List<Vector2Int> RetracePath(Node startNode, Node endNode)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode.Position);
            currentNode = currentNode.Parent;
        }

        path.Reverse();
        return path;
    }

    private float GetDistance(Node a, Node b)
    {
        return Vector2Int.Distance(a.Position, b.Position);
    }
}
