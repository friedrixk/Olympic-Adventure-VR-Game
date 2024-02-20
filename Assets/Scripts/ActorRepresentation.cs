using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCIKonstanz.Colibri.Synchronization;

public class ActorRepresentation : MonoBehaviour
{
    [SerializeField] private GameObject headRepresentation;
    [SerializeField] private GameObject leftHandRepresentation;
    [SerializeField] private GameObject rightHandRepresentation;

    [SerializeField] private GameObject head;
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;

    [SerializeField] private bool isAttached = false;
    void Update()
    {
        if (isAttached) {
            UpdatePosition();
        }
    }
    public void SetIsAttached(bool value) {
        isAttached = value;
    }

    public bool GetIsAttached() {
        return isAttached;
    }

    void UpdatePosition() {
        headRepresentation.transform.position = head.transform.position;
        headRepresentation.transform.rotation = head.transform.rotation;

        leftHandRepresentation.transform.position = leftHand.transform.position;
        leftHandRepresentation.transform.rotation = leftHand.transform.rotation;

        rightHandRepresentation.transform.position = rightHand.transform.position;
        rightHandRepresentation.transform.rotation = rightHand.transform.rotation;
    }
}
