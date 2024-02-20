using System.Collections;
using UnityEngine;

public class AutoRoleSelection : MonoBehaviour
{
    [SerializeField]
    private ActorRepresentation Zeus;

    [SerializeField]
    private ActorRepresentation Poseidon;

    public bool runCheck = true;

    public enum  State {
        Controllers,
        Hands,
        Unknown
    }

    private State currentState = State.Unknown;

    private void Start()
    {
        StartCoroutine(CheckInputMethodRoutine());
    }

    private IEnumerator CheckInputMethodRoutine()
    {
        while (runCheck)
        {
            bool hasControllers = PerformsAnyControllerAction();
            bool hasHands = PerformsAnyHandAction();

            if (hasControllers)
            {
                currentState = State.Controllers;
            }
            else if (!hasControllers && hasHands)
            {
                currentState = State.Hands;
            }

            if (currentState == State.Hands)
            {
                Zeus.SetIsAttached(true);
                Poseidon.SetIsAttached(false);
            }
            else if (currentState == State.Controllers)
            {
                Zeus.SetIsAttached(false);
                Poseidon.SetIsAttached(true);
            }

            yield return new WaitForSeconds(0.5f); // Check every x seconds
        }
    }

    public State GetCurrentState()
    {
        return currentState;
    }

    private bool PerformsAnyHandAction()
    {
        OVRHand[] hands = FindObjectsOfType<OVRHand>();

        foreach (OVRHand hand in hands)
        {
            if (hand.GetFingerIsPinching(OVRHand.HandFinger.Index) || hand.GetFingerIsPinching(OVRHand.HandFinger.Middle))
            {
                return true;
            }
        }
        return false;
    }

    private bool PerformsAnyControllerAction()
    {
        return OVRInput.Get(OVRInput.RawButton.RIndexTrigger) || OVRInput.Get(OVRInput.RawButton.LIndexTrigger) || OVRInput.Get(OVRInput.RawButton.RHandTrigger) || OVRInput.Get(OVRInput.RawButton.LHandTrigger);
    }
}
