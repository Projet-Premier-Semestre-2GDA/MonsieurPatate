using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MembreCollantSupplement : MonoBehaviour
{
    [SerializeField]private MembreCollant membreCollant;
    // Start is called before the first frame update
    void Start()
    {
        //membreCollant = GetComponentInParent<MembreCollant>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        membreCollant.isCollision = true;
        GameObject objectCollision;
        try
        {
            objectCollision = collision.collider.attachedRigidbody.gameObject;
        }
        catch (System.NullReferenceException)
        {
            objectCollision = collision.gameObject;
        }
        Debug.Log(this.name + " touch " + objectCollision.name + " and his tag is " + objectCollision.tag);
        if (!objectCollision.CompareTag(this.tag) && objectCollision != null)
        {
            membreCollant.SetTouch(objectCollision, collision.GetContact(0).point);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        membreCollant.isCollision = false;
        GameObject objectCollision;
        try
        {
            objectCollision = collision.collider.attachedRigidbody.gameObject;
        }
        catch (System.NullReferenceException)
        {
            objectCollision = collision.gameObject;
        }
        Debug.Log(this.name + " don't touch " + objectCollision.name + " and his tag is " + objectCollision.tag);
    }
}
