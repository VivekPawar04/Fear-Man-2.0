using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTouchMovement : MonoBehaviour
{
    public Transform target; // Reference to the player object
    public float rotationSpeed = 10f;
    public float smoothness = 10f;
    public Vector3 offset; // Offset between the camera and the player

    private float verticalRotation = 0f;
    private float horizontalRotation = 0f;
    private Quaternion targetRotation;

    void LateUpdate() // Use LateUpdate for camera movement to ensure it follows the player after their movement
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                float rotateX = -touch.deltaPosition.y * rotationSpeed * Time.deltaTime;
                float rotateY = touch.deltaPosition.x * rotationSpeed * Time.deltaTime;

                verticalRotation += rotateX;
                horizontalRotation += rotateY;

                verticalRotation = Mathf.Clamp(verticalRotation, -80f, 80f);

                targetRotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0f);
            }
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothness * Time.deltaTime);

        // Calculate the desired position of the camera based on the player's position and the offset
        Vector3 desiredPosition = target.position + offset;

        // Smoothly move the camera towards the desired position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothness * Time.deltaTime);
    }
}
