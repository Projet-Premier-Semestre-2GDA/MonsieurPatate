using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bras : Membre
{

    public override void Start()
    {
        base.Start();
    }


    public override void FirstAction()
    {
        base.FirstAction();
    }
    public override void SecondAction()
    {
        base.SecondAction();

    }
    public override void ThirdAction()
    {
        
        directionForce = transform.up;
        rb.AddForce(directionForce * 10);
    }
}
