using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MembreTournant : MembreAForce
{
    public GameObject turnObject;
    HingeJoint hinge;
    Rigidbody rbOther;
    private bool turn = false;

    public override void Start()
    {
        base.Start();
        hinge = turnObject.GetComponent<HingeJoint>();
        rbOther = turnObject.GetComponent<Rigidbody>();
    }

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
        hinge.useMotor = turn;
        if (!turn)
        {
            rbOther.angularVelocity = new Vector3();            
        }
    }

    private void TurnObject(GameObject objectToTurn)
    {
        objectToTurn.transform.localEulerAngles += Vector3.up * puissance * Time.fixedDeltaTime;

    }
}
