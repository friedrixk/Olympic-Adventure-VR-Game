using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSeal : MonoBehaviour
{
    public GameObject ring;

    public bool isHovered = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == ring)
        {
            isHovered = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == ring)
        {
            isHovered = false;
        }
    }
}
