using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCamera : MonoBehaviour
{
    [SerializeField] float distanceMaxWithTarget = 3;
    [SerializeField] float angleMaxAutorisee = 30;
    float distanceMinWithTarget = 2;
    [SerializeField] GameObject target;
    [SerializeField] float speedMovement = 100;
    [SerializeField] float speedRotation = 10;

    // Start is called before the first frame update
    void Start()
    {
        if (distanceMaxWithTarget < 3)
        {
            distanceMaxWithTarget = 3;
        }
        distanceMinWithTarget = distanceMaxWithTarget - 2;
        transform.LookAt(target.transform);

    }

    // Update is called once per frame
    void Update()
    {
        //rotation du joueur
        transform.RotateAround(target.transform.position, Vector3.up, -Input.GetAxis("HorizontalCamera") * speedRotation * Time.unscaledDeltaTime);

        Debug.Log(transform.eulerAngles.x);
        
        if ((transform.eulerAngles.x < angleMaxAutorisee || transform.eulerAngles.x > 350) && Input.GetAxis("VerticalCamera") > 0)
        {
            transform.RotateAround(target.transform.position, transform.right, Input.GetAxis("VerticalCamera") * speedRotation * Time.unscaledDeltaTime);
        }
        if ((transform.eulerAngles.x > 0 && transform.eulerAngles.x < angleMaxAutorisee+10 && Input.GetAxis("VerticalCamera") < 0))
        {
            transform.RotateAround(target.transform.position, transform.right, Input.GetAxis("VerticalCamera") * speedRotation * Time.unscaledDeltaTime);
        }


        transform.LookAt(target.transform.position);

        //changement par le système
        if ((target.transform.position - transform.position).magnitude > 100)
        {
            transform.Translate(transform.forward * 1000 );
        }
        else if ((target.transform.position - transform.position).magnitude > distanceMaxWithTarget)
        {
            transform.Translate(transform.forward * speedMovement );
        }
        else if ((target.transform.position - transform.position).magnitude < distanceMinWithTarget)
        {
            transform.Translate(-transform.forward * speedMovement);
        }
        transform.LookAt(target.transform.position);

        ////test, et ça marche pas

        //if (transform.eulerAngles.x >= angleMaxAutorisee+1 || transform.eulerAngles.x <= 180) 
        //{
        //    transform.RotateAround(target.transform.position, transform.right, -1);
        //}
        //if (transform.eulerAngles.x < 359 && transform.eulerAngles.x > 180)
        //{
        //    transform.RotateAround(target.transform.position, transform.right, 1);
        //}

    }
}
