using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientationAttachPoint : MonoBehaviour
{
    public GameObject center;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 direction = center.transform.position - transform.position ;
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.localEulerAngles += new Vector3(-90, 0, 0);
    }
}
