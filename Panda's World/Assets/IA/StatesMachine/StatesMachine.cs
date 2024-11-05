using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StatesMachine : MonoBehaviour
{
    private State currentState;
    private Vector2Int villageCenter;
    private int villageRadius = 10; // Example radius, adjust as needed
    private Vector2 targetPosition;

    
    private enum State
    {
        Moving,
        Working,
    }
    
    
    // Start is called before the first frame update
    void Start()
    { 
        currentState = State.Moving; 
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case State.Moving:
                Move();
                break;
            // Handle other states as needed
        }

    }
    
    private void Move()
    {
        
    }

    private void SetRandomTargetPosition()
    {
        
    }

}
