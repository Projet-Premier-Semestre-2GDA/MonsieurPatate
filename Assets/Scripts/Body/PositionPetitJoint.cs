using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionPetitJoint : MonoBehaviour
{
    private void FixedUpdate()
    {
        if (transform.localPosition.magnitude > 0.55)
        {
            transform.localPosition = transform.localPosition.normalized * 0.5f;
        }
    }
}
