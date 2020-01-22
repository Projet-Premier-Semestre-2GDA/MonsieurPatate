using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAAccrocheur : IA
{
    private Rigidbody rb;
    public float puissanceSaut = 50;
    public GameObject objectJoint;
    private FixedJoint joint;
    private void Start()
    {
        joint = objectJoint.GetComponent<FixedJoint>();
        objectJoint.SetActive(false);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("membre"))
        {
            Vector3 direction = other.transform.position - transform.position;
            rb.AddForce(direction.normalized * puissanceSaut, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        var other = collision.collider;
        if (other.CompareTag("Player") || other.CompareTag("membre"))
        {
            SeColler(other.gameObject,collision.GetContact(0).point);
        }
    }
    private void SeColler(GameObject objetAColler,Vector3 collisionPoint)
    {
        objectJoint.SetActive(true);
        objectJoint.transform.position = collisionPoint;
        joint.connectedBody = objetAColler.GetComponent<Rigidbody>();
    }
}
