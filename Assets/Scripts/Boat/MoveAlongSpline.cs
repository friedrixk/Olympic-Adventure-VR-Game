using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class MoveAlongSpline : MonoBehaviour
{
    public SplineContainer spline;

    public Vector3 playerInBoatPosition = new Vector3(0, 0, 0);

    public TridentHolder tridentHolder;
    public bool isChained = true;
    public float speed = 1.0f;
    public float distancePercentage = 0.0f;

    public PaddleAnchor paddleFrontLeft;
    public PaddleAnchor paddleFrontRight;
    public PaddleAnchor paddleBackLeft;
    public PaddleAnchor paddleBackRight;

    float splineLength;

    private AutoRoleSelection autoRoleSelection;

    private GameObject player;

    [SerializeField] private MoveAlongSpline boat;
    
    void Start()
    {
        splineLength = spline.CalculateLength();
        autoRoleSelection = FindObjectOfType<AutoRoleSelection>();
        // player = GameObject.Find("OVRCameraRig");
        player = GameObject.Find("Player");  
    }

    void Update()
    {
        if (distancePercentage < 0.99f && !isChained && tridentHolder.holdsTrident)
        {
            bool frontLeftActive = paddleFrontLeft.activationLevel > 0.1f;
            bool frontRightActive = paddleFrontRight.activationLevel > 0.1f;
            bool backLeftActive = true;  //paddleBackLeft.activationLevel > 0.5f;
            bool backRightActive = true; // paddleBackRight.activationLevel > 0.5f;

            if (frontLeftActive && frontRightActive && backLeftActive && backRightActive)
            {
                distancePercentage += speed * Time.deltaTime / splineLength;

                Vector3 currentPosition = spline.EvaluatePosition(distancePercentage);
                transform.position = currentPosition;

                Vector3 nextPosition = spline.EvaluatePosition(distancePercentage + 0.01f);
                Vector3 direction = nextPosition - currentPosition;
                transform.rotation = Quaternion.LookRotation(direction, transform.up);
            } else {
                Debug.Log("Not all paddles are active");
                Debug.Log("frontLeftActive: " + frontLeftActive);
                Debug.Log("frontRightActive: " + frontRightActive);
                Debug.Log("backLeftActive: " + backLeftActive);
                Debug.Log("backRightActive: " + backRightActive);
            }
        }
        if (!isChained) {
            if (autoRoleSelection.GetCurrentState() == AutoRoleSelection.State.Hands)
            {
                player.SetActive(false);
                player.transform.parent = boat.transform;
                player.transform.localPosition = playerInBoatPosition;
                player.SetActive(true);
            }
        }
    }
}
