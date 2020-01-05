using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MembreCollant : Membre
{
    public float puissanceBras = 5;
    public bool isCollision = false;
    private Vector3 pointTouch;
    private GameObject objectTouch;
    private SpringJoint springJoint;
    public GameObject lePetitJoint;

    public override void Start()
    {
        base.Start();
        lePetitJoint.SetActive(false);
    }

    public override void Action(float analogiqueReturn = 1) //Actuellement sans pour autant pouvoir tester je test de créer des joint dans le code et de les gérer puis de les supprimer
    {
        base.Action(analogiqueReturn);

        if (isCollision)
        {
            if (lePetitJoint.activeSelf != true)
            {
                lePetitJoint.SetActive(true);
                Debug.Log("Creation Du Spring Joint");
                springJoint = lePetitJoint.GetComponent<SpringJoint>();
                //Assignation des parametre
                springJoint.connectedBody = objectTouch.GetComponent<Rigidbody>();
                springJoint.anchor = new Vector3(0, 1.1f, 0);
                springJoint.autoConfigureConnectedAnchor = true;
            }
            else
            {
                Debug.Log("Le Joint est déjà appliquee");
                return;
            }

        }
    }
    public override void NonAction()
    {
        base.NonAction();
        lePetitJoint.SetActive(false);
    }

    public void SetTouch(GameObject theTouch, Vector3 thePoint)
    {
        pointTouch = thePoint;
        objectTouch = theTouch;
    }
    
}
