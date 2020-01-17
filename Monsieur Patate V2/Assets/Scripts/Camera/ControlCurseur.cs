using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCurseur : MonoBehaviour
{
    [SerializeField] private float tempsPourParcourirEcran = 1.5f;
    public static Vector2 positionCurseur;
    RectTransform curseur;

    private float horizontalCursorInput;
    private float verticalCursorInput;
    
    void Start() {
        curseur = GetComponent<RectTransform>();
    }
    
    private void LateUpdate()
    {
        if (Input.GetButtonDown("PauseButton"))
        {
            curseur.localPosition = new Vector3(0,0,0);
        }
    }
    void Update() {
        horizontalCursorInput = Input.GetAxis("HorizontalCursor");
        verticalCursorInput = Input.GetAxis("VerticalCursor");

        float h = horizontalCursorInput * (GetComponentInParent<RectTransform>().rect.width / this.tempsPourParcourirEcran * 60);
        float v = verticalCursorInput * (GetComponentInParent<RectTransform>().rect.height / this.tempsPourParcourirEcran * 60);
        
        Vector2 cursorVelocity = new Vector2(h, v);
        
        curseur.Translate(cursorVelocity * Time.unscaledDeltaTime);
        positionCurseur = this.curseur.position;
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
