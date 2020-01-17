using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jambe : MembreAForce
{
    public float puissanceJambe = 10;


    public override void Action(float analogiqueReturn = 1)
    {
        base.Action(analogiqueReturn);
        directionForce = -transform.up;
        rbActive.AddForce(directionForce * puissanceJambe*analogiqueReturn, typeDeForceAppliquee);
    }
}
