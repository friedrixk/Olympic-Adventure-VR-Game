using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftPaddle : MonoBehaviour
{
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material rotatableMaterial;
    [SerializeField] private Material rotatingMaterial;
    [SerializeField] private GameObject controller;

    [SerializeField] private PaddleHandle paddleHandle;

    private float _currentAngularVelocity = 0f;
    private bool _isRotating = false;
    private Renderer _renderer;

    private Vector3 _lastControllerPosition;
    private Vector3 _valvePivot;
    private bool _isFirstMovement = true;

    // private AudioSource audioSource;

    void Awake()
    {
        // audioSource = GetComponent<AudioSource>();
    }

    void Start()
    { 
        _renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (_isRotating)
        {
            UpdateRotation();
        }
        else
        {
            // audioSource.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == controller)
        {
            _renderer.material = rotatableMaterial;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == controller && OVRInput.Get(OVRInput.Button.SecondaryHandTrigger) && paddleHandle.isHovered)
        {
            if (!_isRotating)
            {
                StartRotation();
            }
        }
        else if (_isRotating)
        {
            StopRotation();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == controller)
        {
            if (_isRotating)
            {
                StopRotation();
            }
            _renderer.material = defaultMaterial;
        }
    }

    private void StartRotation()
    {
        _isRotating = true;
        _renderer.material = rotatingMaterial;
        _currentAngularVelocity = 0f;
        _lastControllerPosition = controller.transform.position;
        _isFirstMovement = true;
        _valvePivot = transform.position;
    }


    private void UpdateRotation()
    {
        Vector3 currentControllerPosition = controller.transform.position;

        if (!_isFirstMovement)
        {
            Vector3 previousVector = _lastControllerPosition - _valvePivot;
            Vector3 currentVector = currentControllerPosition - _valvePivot;
            Vector3 crossProduct = Vector3.Cross(previousVector, currentVector);

            float arcLength = Vector3.Distance(_lastControllerPosition, currentControllerPosition);
            float radius = Vector3.Distance(_valvePivot, _lastControllerPosition);

            if (radius > 0)
            {
                float angularChange = arcLength / radius;

                if (angularChange > 0.05f /*&& audioSource.isPlaying == false*/)
                {
                    // audioSource.Play();
                }
                else if (angularChange < 0.05f /*&& audioSource.isPlaying == true*/)
                {
                    // audioSource.Stop();
                }

                // Determine the direction of rotation
                float direction = -1*Mathf.Sign(crossProduct.x);

                // Convert to degrees and calculate angular velocity (degrees per second)
                _currentAngularVelocity = direction * (angularChange * Mathf.Rad2Deg) / Time.deltaTime;
            }
        }
        else
        {
            _isFirstMovement = false;
        }

        _lastControllerPosition = currentControllerPosition;

        transform.Rotate(0, _currentAngularVelocity * Time.deltaTime, 0, Space.Self);
    }

    private void StopRotation()
    {
        _isRotating = false;
        _renderer.material = rotatableMaterial;
    }
}