using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCIKonstanz.Colibri.Synchronization;

public class BallSwitcher : MonoBehaviour
{
    // Start is called before the first frame update

    private AutoRoleSelection autoRoleSelection;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            autoRoleSelection = FindObjectOfType<AutoRoleSelection>();
            if (autoRoleSelection.GetCurrentState() == AutoRoleSelection.State.Controllers) {
                SyncTransform syncTransform = other.GetComponent<SyncTransform>();
                syncTransform.PhysicsAuthority = true;
            }
        }
    }
}
