using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MembreCollant : Membre
{
    //variable liée au secondMembreCollant
    public bool isCollision = false;
    private Vector3 pointTouch;
    private GameObject objectTouch;
    //variable du joint
    private SpringJoint springJoint;
    public GameObject lePetitJoint;
    //Variable pour le script
    private bool buttonActionStillPush = false;
    private bool asAttach = false;
    public override void Start()
    {
        base.Start();
        springJoint = lePetitJoint.GetComponent<SpringJoint>();
        lePetitJoint.SetActive(false);

    }

    public override void Action(float analogiqueReturn = 1) //Actuellement sans pour autant pouvoir tester je test de créer des joint dans le code et de les gérer puis de les supprimer
    {
        base.Action(analogiqueReturn);
        buttonActionStillPush = true;
    }
    public override void NonAction()
    {
        base.NonAction();
        buttonActionStillPush = false;
        lePetitJoint.SetActive(false);
        asAttach = false;
    }

    public void SetTouch(GameObject theTouch, Vector3 thePoint)
    {
        pointTouch = thePoint;
        objectTouch = theTouch;
    }

    protected void FixedUpdate()
    {
        if (buttonActionStillPush)
        {
            if (isCollision || asAttach)
            {
                if (lePetitJoint.activeSelf != true)
                {
                    lePetitJoint.SetActive(true);
                    Debug.Log("Activation Du Spring Joint");
                    //Assignation des parametre
                    springJoint.connectedBody = objectTouch.GetComponent<Rigidbody>();
                    springJoint.anchor = new Vector3(0, 1.1f, 0);
                    springJoint.autoConfigureConnectedAnchor = true;
                    asAttach = true;
                }
                else
                {
                    Debug.Log("Le Joint est déjà appliquee");
                    return;
                }

            }
        }
        
    }

}
