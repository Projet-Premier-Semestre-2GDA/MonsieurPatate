using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAMandragore : IA
{
    public float vitesseDeRentreeDansLeSol;
    private float taillePerso;
    private Vector3 objectif;
    private bool isInGround;
    private bool isOutGround;
    private void Start()
    {
        taillePerso = transform.lossyScale.y;
        objectif = transform.position - new Vector3(0, taillePerso / 2,0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("membre"))
        {
            isInGround = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("membre"))
        {
            isOutGround = true;
        }
    }

    private void GoInGround()
    {

        transform.position = Vector3.MoveTowards(transform.position, objectif, vitesseDeRentreeDansLeSol * Time.deltaTime);
    }
    private void GoOutGround()
    {
        transform.position = Vector3.MoveTowards(transform.position, objectif + new Vector3(0, taillePerso/2,0), vitesseDeRentreeDansLeSol * Time.deltaTime);

    }
    public override void ActionIA()
    {
        base.ActionIA();
    }

    private void Update()
    {
        if (isInGround)
        {
            GoInGround();
        }
        else if(isOutGround)
        {
            GoOutGround();
        }
    }
}
