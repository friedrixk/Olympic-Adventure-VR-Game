using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform destination;
    public GameObject playerg;
    public GameObject collider;
    public HandSeal handSeal;

    private ParticleSystem[] electroHits;

    public GameObject zeusHead;

    private bool teleporting = false;

    void Start()
    {
        electroHits = zeusHead.GetComponentsInChildren<ParticleSystem>(includeInactive: true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            playerg.SetActive(false);
            playerg.transform.position = destination.position;
            playerg.SetActive(true);
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == collider && handSeal.isHovered)
        {
            Debug.Log(other.gameObject.tag);
            Debug.Log(other.gameObject.name);
            
            if (!teleporting){
                teleporting = true;
                StartCoroutine(TeleportPlayer());
            }
        }
    }

    IEnumerator TeleportPlayer()
    {
        foreach (ParticleSystem electroHit in electroHits){
            electroHit.gameObject.SetActive(true);
        }
        
        yield return new WaitForSeconds(2);
        playerg.SetActive(false);
        playerg.transform.position = destination.position;
        playerg.SetActive(true);
        yield return new WaitForSeconds(2);

        foreach (ParticleSystem electroHit in electroHits){
            electroHit.gameObject.SetActive(false);
        }
    }
}
