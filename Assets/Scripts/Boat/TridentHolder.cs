using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TridentHolder : MonoBehaviour
{    
    public bool holdsTrident = false;

    [SerializeField] private GameObject boatTridentParent;

    [SerializeField] private GameObject poseidonsTrident;

    [SerializeField] private GameObject poseidonsTridentParent;

    [SerializeField] private MoveAlongSpline boat;

    private AutoRoleSelection autoRoleSelection;

    private bool wasTpressed = false;

    public Vector3 playerInBoatPosition = new Vector3(0, 0, 0);


    private GameObject player;

    private GameObject boatObject;


    private void Start()
    {
        SetTridentToPoseidon();
        autoRoleSelection = FindObjectOfType<AutoRoleSelection>();
        // player = GameObject.Find("OVRCameraRig");
        player = GameObject.Find("Player");
        // get boat by tag
        if (autoRoleSelection == null)
        {
            Debug.LogError("AutoRoleSelection not found");
        }
        if (player == null)
        {
            Debug.LogError("Player not found");
        }
    }

    private void Update()
    {
        if ((holdsTrident && boat.distancePercentage >= 0.98f))
        {
            SetTridentToBoat();
        }
        if (holdsTrident) {
            if (autoRoleSelection.GetCurrentState() == AutoRoleSelection.State.Controllers)
            {
                player.SetActive(false);
                player.transform.parent = boat.transform;
                player.transform.localPosition = playerInBoatPosition;
                player.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (!wasTpressed)
            {
                Debug.Log("Toggle trident");
                wasTpressed = true;
                if (holdsTrident)
                {
                    Debug.Log("Set to Poseidon");
                    SetTridentToPoseidon();
                } 
                else 
                {
                    Debug.Log("Set to Boat");
                    SetTridentToBoat();
                }
            }
        } else {
            wasTpressed = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trident triggered" + other.gameObject.name);
        if (other.gameObject == poseidonsTrident.gameObject)
        {
            SetTridentToBoat();
        }
    }

    void SetTridentToBoat()
    {
        try
        {
            poseidonsTridentParent.SetActive(false);
            boatTridentParent.SetActive(true);
            GameObject boatObject = GameObject.FindGameObjectWithTag("Boat");
            holdsTrident = true;
            if (autoRoleSelection.GetCurrentState() == AutoRoleSelection.State.Controllers)
            {
                player.transform.parent = boat.transform;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error setting trident to boat: " + ex.Message);
            Debug.LogError(ex.StackTrace);
            Debug.LogError("References: Boat: " + boatTridentParent + ", Poseidon Trident " + poseidonsTridentParent + " " + player + ", Boat Object" + boatObject + ". Over.");
        }
    }

    void SetTridentToPoseidon()
    {
        try
        {
            boatTridentParent.SetActive(false);
            holdsTrident = false;
            poseidonsTridentParent.SetActive(true);
            if (autoRoleSelection.GetCurrentState() == AutoRoleSelection.State.Controllers)
            {
                player.transform.parent = null;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error setting trident to Poseidon: " + ex.Message);
            Debug.LogError(ex.StackTrace);
            Debug.LogError("References: Boat: " + boatTridentParent + ", Poseidon Trident " + poseidonsTridentParent + " " + player + ", Boat Object" + boatObject + ". Over.");
        }
    }
}
