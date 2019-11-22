using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jambe : Membre
{
    public override void Start()
    {
        base.Start();
    }


    public override void FirstAction(Transform secondTransform, Rigidbody secondRb)
    {
        base.FirstAction(secondTransform, secondRb);
        secondTransform.Rotate(Vector3.right, vitesseDeRotation * Time.deltaTime);
    }
    public override void SecondAction(Transform secondTransform, Rigidbody secondRb)
    {
        base.SecondAction(secondTransform, secondRb);
        secondTransform.Rotate(Vector3.right, -vitesseDeRotation * Time.deltaTime);

    }
    public override void ThirdAction(Transform secondTransform, Rigidbody secondRb)
    {
        base.ThirdAction(secondTransform, secondRb);
        directionForce = -secondTransform.up;
        secondRb.AddForce(directionForce * 10);
    }
}
