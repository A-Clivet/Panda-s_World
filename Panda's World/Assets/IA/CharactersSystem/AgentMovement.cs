using UnityEngine;
using UnityEngine.AI;

public class AgentMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 targetPosition;
    private bool hasTarget = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        
        // Positionner l'agent sur le NavMesh
        if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 20f, NavMesh.AllAreas))
        {
            agent.Warp(hit.position);
        }
        else
        {
            Debug.LogError("Impossible de positionner l'agent sur le NavMesh.");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Detect left mouse click
        {
            Debug.Log("Clic de souris détecté.");
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero);
            if (hit.collider != null)
            {
                targetPosition = hit.point;
                hasTarget = true;
            }
            else
            {
                Debug.Log("Le raycast n'a touché aucun collider.");
            }
        }

        if (hasTarget)
        {
            if (agent.isOnNavMesh)
            {
                Debug.Log("Déplacement vers la position cible : " + targetPosition);
                agent.SetDestination(targetPosition);
            }
            else
            {
                Debug.Log("L'agent n'est pas sur le NavMesh.");
            }
        }
    }
}