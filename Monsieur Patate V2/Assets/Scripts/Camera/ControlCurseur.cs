using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCurseur : MonoBehaviour
{
    public float tempsPourParcourirEcran = 1.5f;
    public static Vector2 positionCurseur;
    RectTransform curseur;
    // Start is called before the first frame update
    void Start()
    {
        curseur = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    
    private void LateUpdate()
    {
        if (Input.GetButtonDown("PauseButton"))
        {
            curseur.localPosition = new Vector3(0,0,0);
        }
    }
    void Update()
    {
        float h = Input.GetAxis("HorizontalCursor") * tempsPourParcourirEcran/GetComponentInParent<RectTransform>().rect.width ;
        float v = Input.GetAxis("VerticalCursor") * tempsPourParcourirEcran / GetComponentInParent<RectTransform>().rect.height;



        curseur.Translate(new Vector2(h,v));
        positionCurseur = new Vector2(curseur.position.x, curseur.position.y);
        //Debug.Log("La position du curseur est : " +positionCurseur);
    }

    int InputArrondi(float x, float threeshold = 0.01f)
    {
        if (Mathf.Abs(x) <= threeshold)
        {
            return 0;
        }
        else if (x >0)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }
}
