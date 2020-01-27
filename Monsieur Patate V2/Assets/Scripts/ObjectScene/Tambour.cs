using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tambour : MonoBehaviour
{
    public float puissanceTambour = 20f;
    private GameObject player;
    private Rigidbody playerRb;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerRb = player.GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.attachedRigidbody.CompareTag("Player") || collision.collider.attachedRigidbody.CompareTag("Player"))
        {
            playerRb.AddForce(transform.up * puissanceTambour, ForceMode.Impulse);
        }
    }
}
