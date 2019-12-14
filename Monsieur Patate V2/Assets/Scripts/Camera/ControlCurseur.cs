using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCurseur : MonoBehaviour
{
    public static Vector2 positionCurseur;
    RectTransform curseur;
    // Start is called before the first frame update
    void Start()
    {
        curseur = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        curseur.Translate(new Vector2(Input.GetAxis("HorizontalCursor"), Input.GetAxis("VerticalCursor")));
        positionCurseur = new Vector2(curseur.position.x, curseur.position.y);
        //Debug.Log("La position du curseur est : " +positionCurseur);
    }
}
