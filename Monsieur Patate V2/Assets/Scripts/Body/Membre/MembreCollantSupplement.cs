using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MembreCollantSupplement : MonoBehaviour
{
    private MembreCollant membreCollant;
    // Start is called before the first frame update
    void Start()
    {
        membreCollant = GetComponentInParent<MembreCollant>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        membreCollant.isCollision = true;
        membreCollant.SetTouch(collision.gameObject, collision.GetContact(0).point);
    }

    private void OnCollisionExit(Collision collision)
    {
        membreCollant.isCollision = false;
    }
}
