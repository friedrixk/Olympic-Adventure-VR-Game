using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    public Rigidbody boatRigidbody; // Assign your boat's Rigidbody
    public Transform rightPaddle; // Assign the right paddle's Transform
    public Transform leftPaddle; // Assign the left paddle's Transform
    public float paddleForce = 200f; // Force applied by each paddle stroke
    public float paddleLength = 2f; // Length of the paddle

    private void FixedUpdate()
    {
        // Get input for paddles, assume some method to get these values
        float rightPaddleInput = GetRightPaddleInput();
        float leftPaddleInput = GetLeftPaddleInput();

        // Apply force based on input
        ApplyPaddleForce(rightPaddle, rightPaddleInput);
        ApplyPaddleForce(leftPaddle, leftPaddleInput);
    }

    private void ApplyPaddleForce(Transform paddle, float input)
    {
        // Calculate force direction (this is a simplified example)
        Vector3 forceDirection = paddle.forward * input;

        // Calculate the position to apply the force
        Vector3 forcePosition = paddle.position + paddle.transform.up * paddleLength;

        // Apply the force
        boatRigidbody.AddForceAtPosition(forceDirection * paddleForce, forcePosition);
    }

    private float GetRightPaddleInput()
    {
        // Implement your input method here
        // For example, it could be based on player input, AI, or some other logic
        return Input.GetAxis("RightPaddle");
    }

    private float GetLeftPaddleInput()
    {
        // Implement your input method here
        return Input.GetAxis("LeftPaddle");
    }
}
