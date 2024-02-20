using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using HCIKonstanz.Colibri.Synchronization;

public class Throwable : MonoBehaviour
{

    // Color for when the object is hovered over and selected
    public Color hoverColor = Color.yellow;
    public Color selectColor = Color.red;
    public float throwForceMultiplier = 100f;

    private bool hasGravity = false;

    public Selector[] selectors = new Selector[] {};

    // declare an array of booleans to keep track of which selectors are hovered over
    private bool[] isHovered = new bool[] {};
    
    public int isSelected = -1;
    private Rigidbody rb;
    private List<Vector3> recentPositions = new List<Vector3>(); // To store recent positions
    public Vector3 fixedOffset = new Vector3(0f, 0.073f, 0.02f);

    public enum State {
        Idle,
        Hover,
        Selected
    }

    void Awake()
    {
        isHovered = new bool[selectors.Length];
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null) {
            //rb.isKinematic = true; // Make sure the Rigidbody doesn't move due to physics
            hasGravity = rb.useGravity;
        }

        // Grab all selectors from the scene
        selectors = FindObjectsOfType<CombinedSelector>();
        isHovered = new bool[selectors.Length];
        for (int i = 0; i < selectors.Length; i++)
        {
            isHovered[i] = false;
        }
    }

    public State GetState()
    {
        if (isSelected != -1)
        {
            return State.Selected;
        }
        for (int i = 0; i < isHovered.Length; i++)
        {
            if (isHovered[i])
            {
                return State.Hover;
            }
        }
        return State.Idle;
    }

    void Update()
    {
        DeselectWhenTriggerReleased();
        SelectWithExactlyOneTrigger();
        UpdateParent();
        //UpdatePosition();
        UpdateGravity();
        UpdateHovering();
        TrackMovement();
    }

    void DeselectWhenTriggerReleased()
    {
        if (isSelected != -1 && (isSelected >= selectors.Length || !selectors[isSelected].isTriggered()))
        {
            ThrowObject();
            isSelected = -1;
        }
    }

    void SelectWithExactlyOneTrigger() {
        int count = selectors.Length > isHovered.Length ? isHovered.Length : selectors.Length;
        for (int i = 0; i < count; i++)
        {
            if (isHovered[i] && selectors[i].isTriggered())
            {
                if (isSelected == -1) { 
                    isSelected = i;
                }
                else if (isSelected != i) {
                    isSelected = -1;
                    break;
                }
            }
        }
    } 

    void OnTriggerEnter(Collider other)
    {
        // for (int i = 0; i < selectors.Length; i++)
        // {
        //     if (selectors[i] != null && other.tag == selectors[i].tag && !selectors[i].isTriggered())
        //     {
        //         isHovered[i] = true;
        //         selectors[i].lastUsedObject = gameObject;
        //     }
        // }
        // check if it is a selector -> has component selector
        int count = selectors.Length > isHovered.Length ? isHovered.Length : selectors.Length;
        if (other.gameObject.GetComponent<Selector>() != null)
        {
            ClaimPhysicsAuthority();
            Selector selector = other.gameObject.GetComponent<Selector>();
            if (!selector.isTriggered())
            {
                for (int i = 0; i < count; i++)
                {
                    if (selectors[i] != null && other.tag == selectors[i].tag)
                    {
                        isHovered[i] = true;
                        selectors[i].lastUsedObject = gameObject;
                    }
                }
            }
        }



    }

    void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < selectors.Length; i++)
        {
            if (selectors[i] != null && other.tag == selectors[i].tag)
            {
                isHovered[i] = false;
            }
        }
    }

    void UpdateParent()
    {
        if (isSelected != -1)
        {
            transform.parent = selectors[isSelected].transform;
            transform.localPosition = fixedOffset;
            rb.isKinematic = true;
        }
        else
        {
            transform.parent = null;
            rb.isKinematic = false;
        }
    }

    void UpdateGravity()
    {
        if (isSelected != -1)
        {
            if (rb != null) {
                rb.useGravity = false;
            }
        }
        else if (isHovered.Any(x => x))
        {
            if (rb != null) {
                rb.useGravity = hasGravity;
            }
        }
        else {
            if (rb != null) {
                rb.useGravity = hasGravity;
            }
        }
    }

    void UpdatePosition()
    {
        if (isSelected != -1)
        {
            // Update the position of the object to be a fixed offset from the controller
            transform.position = selectors[isSelected].transform.position + fixedOffset;
        }
    }

    void UpdateHovering()
    {
        for (int i = 0; i < selectors.Length; i++)
        {
            if (isHovered[i])
            {
                if (selectors[i].lastUsedObject != gameObject)
                {
                    Debug.LogWarning("Selector " + selectors[i].name + " isHovered but lastUsedObject is not " + gameObject.name);
                    isHovered[i] = false;
                }
            }
        }
    }

    void TrackMovement()
    {
        if (isSelected != -1)
        {
            // Add the current position to the list
            recentPositions.Add(selectors[isSelected].transform.position);
            if (recentPositions.Count > 10)
            {
                recentPositions.RemoveAt(0); // Keep only the last 10 positions
            }
        }
    }

    void ThrowObject()
    {
        if (rb != null)
        {
            ClaimPhysicsAuthority();
            rb.isKinematic = false; // Allow the Rigidbody to be affected by physics
            Vector3 throwDirection = CalculateThrowDirection();
            rb.AddForce(throwDirection * throwForceMultiplier, ForceMode.Impulse);
        }
        recentPositions.Clear();
    }

    public void ClaimPhysicsAuthority()
    {
        SyncTransform syncTransform = GetComponent<SyncTransform>();
        if (syncTransform != null) {
            syncTransform.PhysicsAuthority = true;
        } else {
            Debug.Log("No SyncTransform component found on " + gameObject.name);
        }
    }

    Vector3 CalculateThrowDirection()
    {
        if (recentPositions.Count < 2) return Vector3.zero;

        Vector3 latestPosition = recentPositions[recentPositions.Count - 1];
        Vector3 previousPosition = recentPositions[recentPositions.Count - 2];
        return latestPosition - previousPosition; // Direction is the difference between the last two positions
    }

}
