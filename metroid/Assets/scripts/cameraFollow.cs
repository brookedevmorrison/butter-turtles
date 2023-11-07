using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform player; // Reference to player object

    public float smoothSpeed = 0.125f; // Smoothing factor
    public Vector3 offset; // Offset to control the camera position

    void LateUpdate()
    {
        if (player != null)
        {
            // Calculate the desired position for the camera
            Vector3 desiredPosition = player.position + offset;

            // Use SmoothDamp to smoothly move the camera towards the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            // Ensure the camera's Z position is the same (for 2D)
            transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
        }
    }
}
