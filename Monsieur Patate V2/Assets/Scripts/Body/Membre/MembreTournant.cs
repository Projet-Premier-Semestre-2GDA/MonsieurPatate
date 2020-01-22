using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MembreTournant : MembreAForce
{
    public GameObject turnObject;
    private bool turn = false;
    public override void Action(float analogiqueReturn = 1)
    {
        base.Action(analogiqueReturn);
        //turnObject.transform.Rotate(Vector3.forward * puissance, Space.Self);
        //TurnObject(turnObject);

        turn = true;
    }
    public override void NonAction()
    {
        base.NonAction();
        turn = false;
    }
    private void FixedUpdate()
    {
        if (turn)
        {
            TurnObject(turnObject);
        }
    }

    private void TurnObject(GameObject objectToTurn)
    {
        objectToTurn.transform.localEulerAngles += Vector3.up * puissance * Time.fixedDeltaTime;

    }
}
