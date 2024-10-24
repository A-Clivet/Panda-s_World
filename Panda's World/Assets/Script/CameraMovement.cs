using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    
    public Camera _camera;
    public float speed = 5f;

    // Update is called once per frame
    void Update()
    {
        Move();
        _camera.transform.position = transform.position;
    }


    void Move() // rework le systeme de deplacement plus tard pour qu'il soit plus "ouvert" et qu'on puisse le modifier plus facilement
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(x, y, 0);
        transform.position += move * (Time.deltaTime * speed);
    }
}
