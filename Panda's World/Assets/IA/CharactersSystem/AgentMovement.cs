using System.Collections.Generic;
using UnityEngine;

public class AgentMovement : MonoBehaviour
{
    public float speed;
    
    
    private List<Vector2Int> path;
    private int currentPathIndex;
    private TilemapGenerator tilemapGenerator;
    private ResourceManager resourceManager;


    private void Start()
    {
        tilemapGenerator = FindObjectOfType<TilemapGenerator>();
        resourceManager = FindObjectOfType<ResourceManager>();

        // if (resourceManager is not null && resourceManager.DiamondPos.Count > 0)
        // {
        //     SetDestination(resourceManager.DiamondPos[0]);
        //     resourceManager.DiamondPos.RemoveAt(0);
        // }
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