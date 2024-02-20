using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PaddleAnchor : MonoBehaviour
{
    [SerializeField] private GameObject paddle;
    [SerializeField] private PaddleHandle handle;

    private float lastRotation = 0.0f;

    public float activationLevel = 0.0f;

    public void Start()
    {
        if (paddle == null)
        {
            Debug.LogError("Paddle not found");
        }
        if (handle == null)
        {
            Debug.LogError("PaddleHandle not found");
        }
        lastRotation = paddle.transform.rotation.eulerAngles.y;
    }
    public void Update()
    {
        if (handle.isHeld)
        {
            paddle.transform.LookAt(handle.getSelectorPosition());
        }
    }

    public void FixedUpdate()
    {
        if (handle.isHeld || handle.isActivlyUsed)
        {
            UpdateActivationLevel();
        }
    }

    public void UpdateActivationLevel()
    {
        // calculate how much the paddle has been rotated on y axis
        float rotation = paddle.transform.rotation.eulerAngles.y;
        float rotationDelta = rotation - lastRotation;
        lastRotation = rotation;

        // scale movement to 0-1
        activationLevel = activationLevel + Mathf.Abs(rotationDelta) / 50.0f;
        activationLevel *= 0.9f;
        activationLevel = Mathf.Clamp(activationLevel, 0.0f, 1.0f);

        handle.isActivlyUsed  = activationLevel > 0.1f;
    }


    public Quaternion GetPaddleRotation()
    {
        return paddle.transform.rotation;
    }
}
