using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MembreSansAction : Membre
{
    public override void Action(float analogiqueReturn = 1)
    {
        base.Action(analogiqueReturn);
        Debug.LogWarning(this.name + " ne fais rien et c'est normal.");
    }
}
