using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choc : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Collision");
    }
}
