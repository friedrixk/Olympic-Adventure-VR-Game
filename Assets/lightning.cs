using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightning : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;

    [SerializeField] private float lifetime = 2.0f;

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            // Deactivate then destroy all children, then self
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
                Destroy(child.gameObject);
            }
            Destroy(gameObject);
        }
    }
}
