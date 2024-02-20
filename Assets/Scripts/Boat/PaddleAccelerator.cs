using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PaddleAnchor))]
public class PaddleAccelerator : MonoBehaviour
{

    private enum Side {
        Left,
        Right
    }
    [SerializeField] private float minAngleForDiving = 18.0f;
    [SerializeField] private float maxAngleForDiving = 52.0f;

    [SerializeField] private float frontSwingAngle = 30.0f;
    [SerializeField] private float backSwingAngle = 30.0f;

    // Deeper divining angle -> more force
    // front swing: front most angle for paddle to go in the water
    // back swing: back most angle for paddle to go in the water
    // This class calcualtes the acceleration of the paddle
    // based on the diving angle and the swing change
    [SerializeField] private Side side = Side.Left;

    private float lastDivingDegree = 0.0f;
    private float lastSwingPercentage = 0.0f;

    private PaddleAnchor paddleAnchor;

    void Start()
    {
        paddleAnchor = GetComponent<PaddleAnchor>();
    }

    public float GetAcceleration()
    {
        float divingDegree = GetPaddleDivingDegree();
        float swingDifference = GetSwingDifference();

        float acceleration = divingDegree * swingDifference;

        return acceleration;
    }

    public float GetPaddleDivingDegree()
    {
        // X axis rotation is how much the paddle is in the water
        // more than maxAngleForDiving means the paddle is underwater, full force
        // less than minAngleForDiving means the paddle is above water, no force
        // in between is a linear interpolation
        float angle = paddleAnchor.GetPaddleRotation().eulerAngles.x;
        return AutoNormalize(angle, minAngleForDiving, maxAngleForDiving);
    }

    public float GetSwingDifference()
    {
        // Y axis rotation is how much the paddle is swinging
        // more than maxSwingAngle means the paddle is swinging forward, full force
        // less than minSwingAngle means the paddle is swinging backward, no force
        // in between is a linear interpolation
        Vector3 eulerAngles = paddleAnchor.GetPaddleRotation().eulerAngles;
        float swingPercentage = AutoNormalize(eulerAngles.y, frontSwingAngle, backSwingAngle);
        float swingDifference = swingPercentage - lastSwingPercentage;
        lastSwingPercentage = swingPercentage;
        return swingDifference;
    }

    public static float AutoNormalize(float value, float a, float b)
    {
        float clampedValue = AutoClamp(value, a, b);
        return (clampedValue - a) / (b - a);
    }

    public static float AutoClamp(float value, float a, float b)
    {
        if (a < b)
        {
            return Mathf.Clamp(value, a, b);
        }
        else
        {
            return Mathf.Clamp(value, b, a);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
