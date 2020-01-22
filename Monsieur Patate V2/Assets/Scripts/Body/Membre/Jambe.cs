using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jambe : Membre
{
    public float puissanceJambe = 10;
    private bool onCollision = false;


    public override void Action(float analogiqueReturn = 1)
    {
        if (Time.timeScale == 0)
        {
            analogiqueReturn = 0;
        }
        if (onCollision)
        {
            directionForce = -transform.up;
            rbActive.AddForce(directionForce * puissanceJambe * analogiqueReturn, typeDeForceAppliquee);
        }
        
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("sol") || collision.collider.CompareTag("canJump"))
        {
            onCollision = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        onCollision = false;
    }
}
