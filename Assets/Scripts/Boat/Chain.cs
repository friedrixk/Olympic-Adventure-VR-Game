using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour
{
    public MoveAlongSpline moveAlongSpline;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Lightning")
        {
            moveAlongSpline.isChained = false;
            gameObject.SetActive(false);
        }
    }
}
