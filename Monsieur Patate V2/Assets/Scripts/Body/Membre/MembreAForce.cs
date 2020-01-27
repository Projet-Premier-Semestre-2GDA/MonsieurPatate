using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MembreAForce : Membre
{
    public ForceMode typeDeForceAppliquee = ForceMode.Force;
    public float puissance = 10f;
    public bool applyForceToThisMembre = true;

    public override void Start()
    {
        base.Start();
        if (applyForceToThisMembre)
        {
            rbActive = rb;
        }
        else
        {
            rbActive = rbPlayer;
        }
    }
}
