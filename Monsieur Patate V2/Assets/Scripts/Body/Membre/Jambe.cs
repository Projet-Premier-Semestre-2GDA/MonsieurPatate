using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jambe : Membre
{
    public float puissanceJambe = 10;
    


    public override void Action(float analogiqueReturn = 1)
    {
        if (Time.timeScale == 0)
        {
            analogiqueReturn = 0;
        }
        directionForce = -transform.up;
        rb.AddForce(directionForce * puissanceJambe*analogiqueReturn);
    }
}
