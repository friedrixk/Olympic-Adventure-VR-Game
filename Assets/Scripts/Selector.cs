using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public abstract class Selector : MonoBehaviour
{   
    protected bool wasTriggeredLastFrame = false;
    protected bool wasSecondaryTriggeredLastFrame = false;



    public abstract bool isTriggered();

    public GameObject lastUsedObject = null;

    public virtual bool isSecondaryTriggered()
    {
        return false;
    }
}
