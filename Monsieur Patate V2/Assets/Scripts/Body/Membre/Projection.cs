using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projection : Membre
{
    public float puissanceJambe = 10;
    public Rigidbody autreTruc;


    public override void Action(float analogiqueReturn = 1)
    {
        base.Action(analogiqueReturn);
        directionForce = transform.up;
        autreTruc.AddForce(directionForce * puissanceJambe*analogiqueReturn, typeDeForceAppliquee);
    }
}
