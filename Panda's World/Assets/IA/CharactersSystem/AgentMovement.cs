using System.Collections.Generic;
using UnityEngine;

public class AgentMovement : MonoBehaviour
{
    public float speed = 5f;
    private List<Vector2Int> path;
    private int currentPathIndex;
    private TilemapGenerator tilemapGenerator;

    private void Start()
    {
        tilemapGenerator = FindObjectOfType<TilemapGenerator>();
    }

    private void Update()
    {
        if (path != null && currentPathIndex < path.Count)
        {
            Vector3 targetPosition = new Vector3(path[currentPathIndex].x, path[currentPathIndex].y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                currentPathIndex++;
            }
        }
    }

    public void SetDestination(Vector2Int destination)
    {
        Vector2Int start = new Vector2Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y));
        path = tilemapGenerator.GetPathForUnit(start, destination);
        currentPathIndex = 0;
    }
}