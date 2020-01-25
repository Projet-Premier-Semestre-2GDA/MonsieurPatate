using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP : MonoBehaviour
{
    [HideInInspector]public GameObject personnage;
    public Vector3[] pointDeTp = new Vector3[6];
    private void Start()
    {
        personnage = GameObject.FindGameObjectWithTag("Player");
        GameObject[] test = GameObject.FindGameObjectsWithTag("PointTp");
        for (int i = 0; i < test.Length; i++)
        {
            if (i < 6)
            {
                pointDeTp[i] = test[i].transform.position;
            }
        }
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            personnage.transform.position = pointDeTp[0];
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            personnage.transform.position = pointDeTp[1];
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            personnage.transform.position = pointDeTp[2];
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            personnage.transform.position = pointDeTp[3];
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            personnage.transform.position = pointDeTp[4];
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            personnage.transform.position = pointDeTp[5];
        }
    }

}
