using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MembreTournant : MembreAForce
{
    public GameObject turnObject;
    public override void Action(float analogiqueReturn = 1)
    {
        base.Action(analogiqueReturn);
        turnObject.transform.Rotate(Vector3.right * puissance, Space.Self);
    }
}
