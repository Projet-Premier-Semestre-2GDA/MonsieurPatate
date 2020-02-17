using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bras : MembreAForce
{
    //public float puissanceBras = 5;


    public override void Action(float analogiqueReturn = 1)
    {
        if (Time.timeScale == 0)
        {
            analogiqueReturn = 0;
        }
        directionForce = transform.up;
        rbActive.AddForce(directionForce * puissance * analogiqueReturn, typeDeForceAppliquee);
    }
}
