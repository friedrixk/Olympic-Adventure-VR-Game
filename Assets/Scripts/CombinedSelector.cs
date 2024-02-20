using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SelectorSide
{
    Left,
    Right
}

[RequireComponent(typeof(OVRHand))]
public class CombinedSelector : Selector
{
    [SerializeField]
    private SelectorSide side;

    private OVRHand hand;

    private void Start()
    {
        hand = GetComponent<OVRHand>();
    }
    public override bool isTriggered()
    {
        if (isPinched()) {
            return true;
        } else if (side == SelectorSide.Right) {
            return OVRInput.Get(OVRInput.RawButton.RIndexTrigger);
        }
        else if (side == SelectorSide.Left) {
            return OVRInput.Get(OVRInput.RawButton.LIndexTrigger);
        }
        else {
            return false;
        }
    }


    public bool isPinched()
    {
        return hand.GetFingerIsPinching(OVRHand.HandFinger.Index);
    }

    public bool isMiddleFingerPinched()
    {
        return hand.GetFingerIsPinching(OVRHand.HandFinger.Middle);
    }

    public override bool isSecondaryTriggered()
    {
        if (isMiddleFingerPinched()) {
            return true;
        } else if (side == SelectorSide.Right) {
            return OVRInput.Get(OVRInput.RawButton.RHandTrigger);
        }
        else if (side == SelectorSide.Left) {
            return OVRInput.Get(OVRInput.RawButton.LHandTrigger);
        }
        else {
            return false;
        }
    }
}