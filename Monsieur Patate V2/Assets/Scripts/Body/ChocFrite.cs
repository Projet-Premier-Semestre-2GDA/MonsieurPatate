using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChocFrite : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.collider.attachedRigidbody.CompareTag("Player") || collision.collider.attachedRigidbody.CompareTag("membre"))
        //{
        //}
        FMODUnity.RuntimeManager.PlayOneShot("event:/Nouvelles Frites");

    }
}
