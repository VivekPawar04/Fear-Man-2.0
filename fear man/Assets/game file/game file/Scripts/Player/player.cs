using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float smoothDampTime = 5f;

    private float currentVelocityX;
    private float currentVelocityZ;

    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        // Get input values with smoothing
        float inputX = SmoothInput(ref currentVelocityX, CrossPlatformInputManager.GetAxis("Horizontal"));
        float inputZ = SmoothInput(ref currentVelocityZ, CrossPlatformInputManager.GetAxis("Vertical"));

        // Calculate movement vector
        Vector3 movement = new Vector3(inputX, 0f, inputZ) * moveSpeed;

        // Apply movement to character controller
        characterController.Move(movement * Time.fixedDeltaTime);
    }

    private float SmoothInput(ref float current, float target)
    {
        // Gradually change the current value to the target value over time
        current = Mathf.SmoothDamp(current, target, ref currentVelocityX, smoothDampTime);
        return current;
    }
}
