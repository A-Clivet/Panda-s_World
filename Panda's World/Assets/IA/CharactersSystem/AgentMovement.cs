using UnityEngine;
using UnityEngine.AI;

public class AgentMovement : MonoBehaviour
{
    private Vector3 targetPosition;
    private bool hasTarget = false;

    void Start()
    {
        
      
    }

    void Update()
    {
        // if (Input.GetMouseButtonDown(0)) // Detect left mouse click
        // {
        //     Debug.Log("Clic de souris détecté.");
        //     Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //     RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero);
        //     if (hit.collider != null)
        //     {
        //         targetPosition = hit.point;
        //         hasTarget = true;
        //     }
        //     else
        //     {
        //         Debug.Log("Le raycast n'a touché aucun collider.");
        //     }
        //}
        //
        // if (hasTarget)
        // {
        //     
        // }
    }
}