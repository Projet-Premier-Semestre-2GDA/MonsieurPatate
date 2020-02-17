using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MembreExtender : MembreAForce
{
    public float marge = 0.01f;
    public GameObject movingPart;
    public Transform objectif;
    public Transform beginPosition;
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
                    Move(objectif.position);
                    if (Vector3.Distance(movingPart.transform.position, objectif.position) <= marge)
                    {
                        firstMovementDone = true;
                    }
                }
                else
                {
                    Move(beginPosition.position);
                    if (Vector3.Distance(movingPart.transform.position, beginPosition.position) <= marge)
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
        movingPart.transform.position = Vector3.MoveTowards(movingPart.transform.position, objectifCible, puissance * Time.fixedDeltaTime);
        //rbOther.MovePosition(Vector3.MoveTowards(movingPart.transform.position, objectifCible, puissance * Time.fixedDeltaTime));
        //hinge.connectedAnchor = Vector3.MoveTowards(hinge.connectedAnchor, objectifCible, puissance * Time.fixedDeltaTime);
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
