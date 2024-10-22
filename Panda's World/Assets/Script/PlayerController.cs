using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CameraMovement))]
public class PlayerController : MonoBehaviour
{
    [Tooltip("The ID serve to know which player it is.")]
    private int _id = 1;
    public int ID
    {
        get { return _id; }
        set { _id = value; }
    }

    private ControlScheme _controlScheme;

    private CameraMovement _cameraMovement; // script de mon objet qui bouge

    private void Awake()
    {
        _cameraMovement = GetComponent<CameraMovement>();
    }
    
    private void Update()
    {
        HandleInputs();
    }

    private void HandleInputs()
    {
        JumpingInput();
        MovementInput();
        FireInput();
    }

    private void JumpingInput()
    {
        if (CustomInputSystem.GetKeyDown("Jump" + _id.ToString(), _controlScheme))
        {
            //_cameraMovement.Jump();
        }
    }

    private void MovementInput()
    {
        float movementX = CustomInputSystem.GetAxis("Horizontal" + _id.ToString(), _controlScheme);
            //_cameraMovement.MovementInput = movementX;
    }

    private void FireInput()
    {
        if (CustomInputSystem.GetKeyDown("Fire" + _id.ToString(), _controlScheme))
        {
            Debug.Log("Fire !");
        }
    }
    
}