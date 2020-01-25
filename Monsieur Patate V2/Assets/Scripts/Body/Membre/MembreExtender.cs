using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MembreExtender : MembreAForce
{
    public float marge = 0.01f;
    public GameObject movingPart;
    public Vector3 objectif = new Vector3(0,0.9f,0);
    public Vector3 beginPosition = new Vector3(0,0.21f,0);
    private bool firstMovementDone = false;

    private bool doAction = false;
    private bool canGo = false;
    [SerializeField] private float restTime = 0.5f;
    private float nextAuthorizedTime;

    HingeJoint hinge;
    public override void Start()
    {
        base.Start();
        //movingPart.transform.transform.position = beginPosition.position;
        //movingPart.SetActive(false);
        hinge = movingPart.GetComponent<HingeJoint>();

    }

    private void FixedUpdate()
    {
        if (canGo)
        {
            if (doAction)
            {

                if (!firstMovementDone)
                {
                    Move(objectif);
                    if (Vector3.Distance(hinge.connectedAnchor, objectif) <= marge)
                    {
                        firstMovementDone = true;
                    }
                }
                else
                {
                    Move(beginPosition);
                    if (Vector3.Distance(hinge.connectedAnchor, beginPosition) <= marge)
                    {
                        //movingPart.SetActive(false);
                        firstMovementDone = false;
                        canGo = false;
                        nextAuthorizedTime = Time.time + restTime;

                    }
                }
            }
            
        }
        else
        {
            if (Time.time >= nextAuthorizedTime)
            {
                canGo = true;
            }
        }

    }
    private void Move(Vector3 objectifCible)
    {
        //movingPart.transform.position = Vector3.MoveTowards(movingPart.transform.position, objectifCible, puissanceJambe * Time.fixedDeltaTime);
        //rbOther.MovePosition(Vector3.MoveTowards(movingPart.transform.position, objectifCible, puissance * Time.fixedDeltaTime));
        hinge.connectedAnchor = Vector3.MoveTowards(hinge.connectedAnchor, objectifCible, puissance * Time.fixedDeltaTime);
    }

    public override void Action(float analogiqueReturn = 1)
    {
        base.Action(analogiqueReturn);
        doAction = true;

    }
    public override void NonAction()
    {
        base.NonAction();
        doAction = false;
    }
}
