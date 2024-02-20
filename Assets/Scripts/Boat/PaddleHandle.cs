using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleHandle : MonoBehaviour
{
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material hoverMaterial;

    [SerializeField] private Material activlyUsedMaterial;

    [SerializeField] private Material heldMaterial;
    [SerializeField] public Selector controller;
    public bool isHovered = false;

    public bool isHeld = false;

    private bool wasTriggered = false;

    public bool isActivlyUsed = false;

    private Renderer _renderer;

    public void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void Update()
    {
        bool isTriggered = controller.isSecondaryTriggered();
        bool clicked = isTriggered && !wasTriggered;
        if (isHovered && clicked)
        {
            isHeld = true;
            UpdateColor();
        }
        else if (isHeld && !isTriggered)
        {
            isHeld = false;
            UpdateColor();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == controller.gameObject)
        {
            isHovered = true;
            UpdateColor();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == controller.gameObject)
        {
            isHovered = false;
            UpdateColor();
        }
    }

    public Vector3 getSelectorPosition()
    {
        return controller.transform.position;
    }


    public void UpdateColor()
    {
        if (isActivlyUsed)
        {
            _renderer.material = activlyUsedMaterial;
        }
        else if (isHeld)
        {
            _renderer.material = heldMaterial;
        }
        else if (isHovered)
        {
            _renderer.material = hoverMaterial;
        }
        else
        {
            _renderer.material = defaultMaterial;
        }
    }
}
