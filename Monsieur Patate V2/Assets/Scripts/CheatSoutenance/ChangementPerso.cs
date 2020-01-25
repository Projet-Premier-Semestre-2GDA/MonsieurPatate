using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Cameras;

public class ChangementPerso : MonoBehaviour
{
    private GameObject actualOoska;
    public GameObject[] differentOoska;
    public TP tp;
    public FreeLookCam myCamera;

    private void Start()
    {
        actualOoska = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            ChangementDeOoska(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangementDeOoska(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangementDeOoska(2);
        }
        
    }

    private void ChangementDeOoska(int index)
    {
        Vector3 oldPosition = actualOoska.transform.position;
        Destroy(actualOoska);
        changeOoska(differentOoska[index], oldPosition);
    }

    private void changeOoska(GameObject newOoska, Vector3 aPosition)
    {
        GameObject ooska = Instantiate(newOoska);
        ooska.transform.position = aPosition;
        actualOoska = ooska;
        tp.personnage = ooska;
        myCamera.Target = ooska.transform;
    }

}
