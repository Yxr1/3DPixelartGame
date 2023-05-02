using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float followDelay = 0.1f;  
    public Transform playerBody;      // The delay for the camera to follow the player
    private Vector3 velocity = Vector3.zero; // The current velocity of the camera

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   // Lock the mouse cursor to the game window
    }

    void LateUpdate()
    {
        // Follow the player with a delay
        Vector3 targetPosition = playerBody.position;
        targetPosition.y = transform.position.y;
        targetPosition.z = targetPosition.z - 7;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, followDelay);
    }
}
