using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MembreExtender : Membre
{
    public float marge = 0.01f;
    public float puissanceJambe = 10;
    public GameObject movingPart;
    public Transform objectif;
    public Transform beginPosition;
    private bool firstMovementDone = false;

    public override void Start()
    {
        base.Start();
        movingPart.transform.transform.position = beginPosition.position;
        movingPart.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (movingPart.activeSelf)
        {
            if (!firstMovementDone)
            {
                Move(objectif.position);
                if (Vector3.Distance(movingPart.transform.position,objectif.position) <= marge)
                {
                    firstMovementDone = true;
                }
            }
            else
            {
                Move(beginPosition.position);
                if (Vector3.Distance(movingPart.transform.position, beginPosition.position) <= marge)
                {
                    movingPart.SetActive(false);
                    firstMovementDone = false;
                }
            }
        }
    }
    private void Move(Vector3 objectifCible)
    {
        movingPart.transform.position = Vector3.MoveTowards(movingPart.transform.position, objectifCible, puissanceJambe * Time.fixedDeltaTime);
    }

    public override void Action(float analogiqueReturn = 1)
    {
        base.Action(analogiqueReturn);
        if (!movingPart.activeSelf)
        {
            movingPart.SetActive(true);
            firstMovementDone = false;
        }
        
    }
}
