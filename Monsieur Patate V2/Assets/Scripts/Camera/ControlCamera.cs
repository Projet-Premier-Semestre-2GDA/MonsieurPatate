using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCamera : MonoBehaviour
{
    [SerializeField] float distanceWithTarget = 3;
    [SerializeField] GameObject target;
    [SerializeField] float speedMovement = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(target.transform);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire2"))
        {
            transform.Translate(new Vector3(-Input.GetAxis("Mouse X"),0,0), Space.Self);
            transform.Translate(new Vector3(0,Input.GetAxis("Mouse Y"), 0), Space.Self);
        }
        
        if (Vector3.Distance(transform.position,target.transform.position) > distanceWithTarget)
        {
            transform.Translate((target.transform.position - transform.position).normalized * speedMovement * Time.deltaTime);
        }
    }
}
