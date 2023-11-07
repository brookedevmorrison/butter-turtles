using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Morrison,Brooke 
/// Melendrez, Servando
/// 11/7/23
/// This script makes the camera have dynamic panning
/// </summary>
public class cameraFollow : MonoBehaviour
{
    public Transform player; // Reference to player object

    public float smoothSpeed = 0.125f; // Smoothing factor
    public Vector3 offset; // Offset to control the camera position
    /// <summary>
    /// Allows the camera to offset itself
    /// </summary>
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
