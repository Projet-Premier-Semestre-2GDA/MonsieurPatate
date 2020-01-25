using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choc : MonoBehaviour
{
    public float waitTime = 0f;
    public float randomAdd = 0;
    private bool canPlay;
    private float nextTime = 0;
    private void OnCollisionEnter(Collision collision)
    {
        if (canPlay)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Collision");
            canPlay = false;
            nextTime = Time.time + waitTime + Random.Range(-randomAdd,randomAdd);
        }
    }

    private void Update()
    {
        if (Time.time >= nextTime && !canPlay)
        {
            canPlay = true;
        }
    }
}
