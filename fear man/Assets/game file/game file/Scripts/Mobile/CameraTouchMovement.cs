using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTouchMovement : MonoBehaviour
{
    public float rotationSpeed = 10f;
    public float smoothness = 10f;

    private float verticalRotation = 0f;
    private float horizontalRotation = 0f;

    private Quaternion targetRotation;

    void Update()
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

                // Limit the vertical rotation to prevent over-rotation
                verticalRotation = Mathf.Clamp(verticalRotation, -80f, 80f);

                targetRotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0f);
            }
        }

        // Smoothly rotate the camera towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothness * Time.deltaTime);
    }

}
