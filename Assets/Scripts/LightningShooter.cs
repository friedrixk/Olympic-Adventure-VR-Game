using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCIKonstanz.Colibri.Synchronization;

public class LightningShooter : MonoBehaviour
{
    [SerializeField] private GameObject lightningPrefab;

    [SerializeField] private OVRHand hand;

    [SerializeField] private GameObject centerEyeAnchor;

    [SerializeField] private AudioSource lightningBeamSound;

    private bool isFirstPinch = true;

    void Update()
    {
        if (hand.GetFingerIsPinching(OVRHand.HandFinger.Index))
        {
            if (isFirstPinch)
            {
                lightningBeamSound.Play();
                isFirstPinch = false;
                OVRSkeleton skeleton = hand.GetComponent<OVRSkeleton>();
                OVRBone bone = skeleton.Bones[(int)OVRSkeleton.BoneId.Hand_IndexTip];
                Vector3 indexTipPosition = bone.Transform.position;

                Vector3 eyeAnchorPosition = centerEyeAnchor.transform.position;
                Vector3 direction = indexTipPosition - eyeAnchorPosition;
                GameObject lightning = Instantiate(lightningPrefab, indexTipPosition, Quaternion.LookRotation(direction, Vector3.up));
                // get physics authority
                SyncTransform syncTransform = lightning.GetComponent<SyncTransform>();
                syncTransform.PhysicsAuthority = true;
            }   
            return;
        }
        isFirstPinch = true;
    }
}
