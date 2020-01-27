using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMembre : MonoBehaviour
{
    public Membre membreControlee;
    Rigidbody rb;
    Color selfColor;
    public int numeroMembre = -1;
    KeyCode KeyFirstAction = KeyCode.A;
    KeyCode KeySecondAction = KeyCode.Q;
    KeyCode KeyThirdAction = KeyCode.W;

    void Start()
    {
        transform.tag = "membre";
        AssignationTouche();
        rb = GetComponentInParent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {
        if (numeroMembre == -1)
        {
            AssignationTouche();
        }

        if (Input.GetKey(KeyFirstAction))
        {
            membreControlee.FirstAction();
            Debug.Log("l'objet numero " + numeroMembre + " effectue la première action");
        }
        if (Input.GetKey(KeySecondAction))
        {
            membreControlee.SecondAction();
            Debug.Log("l'objet numero " + numeroMembre + " effectue la deuxième action");
        }
        if (Input.GetKey(KeyThirdAction))
        {
            membreControlee.ThirdAction();
            Debug.Log("l'objet numero " + numeroMembre + " effectue la troisième action");
        }
    }


    public void AssignationTouche()
    {
        switch (numeroMembre)
        {
            case 1:
                {
                    KeyFirstAction = KeyCode.A;
                    KeySecondAction = KeyCode.Q;
                    KeyThirdAction = KeyCode.W;
                    foreach (var item in gameObject.GetComponentsInChildren<MeshRenderer>(false))
                    {
                        item.material.color = (Color.red + Color.yellow) / 2;
                    }
                    
                }
                break;
            case 2:
                {
                    KeyFirstAction = KeyCode.Z;
                    KeySecondAction = KeyCode.S;
                    KeyThirdAction = KeyCode.X;
                    SetColor(Color.red);
                }
                break;
            case 3:
                {
                    KeyFirstAction = KeyCode.E;
                    KeySecondAction = KeyCode.D;
                    KeyThirdAction = KeyCode.C;
                    SetColor(Color.magenta);

                }
                break;
            case 4:
                {
                    KeyFirstAction = KeyCode.R;
                    KeySecondAction = KeyCode.F;
                    KeyThirdAction = KeyCode.V;
                    SetColor((Color.red + Color.blue + Color.blue)/3);

                }
                break;
            case 5:
                {
                    KeyFirstAction = KeyCode.T;
                    KeySecondAction = KeyCode.G;
                    KeyThirdAction = KeyCode.B;
                    SetColor(Color.blue);

                }
                break;
            case 6:
                {
                    KeyFirstAction = KeyCode.Y;
                    KeySecondAction = KeyCode.H;
                    KeyThirdAction = KeyCode.N;
                    SetColor(Color.cyan);

                }
                break;
            case 7:
                {
                    KeyFirstAction = KeyCode.U;
                    KeySecondAction = KeyCode.J;
                    KeyThirdAction = KeyCode.Comma;
                    SetColor((Color.blue + Color.green)/2);

                }
                break;
            case 8:
                {
                    KeyFirstAction = KeyCode.I;
                    KeySecondAction = KeyCode.K;
                    KeyThirdAction = KeyCode.Semicolon;
                    SetColor(Color.green);

                }
                break;
            case 9:
                {
                    KeyFirstAction = KeyCode.O;
                    KeySecondAction = KeyCode.L;
                    KeyThirdAction = KeyCode.Colon;
                    SetColor((Color.green+Color.yellow)/2);

                }
                break;
            default:
                break;
        }
    }

    public void SetColor(Color couleur)
    {
        foreach (var item in gameObject.GetComponentsInChildren<MeshRenderer>(false))
        {
            item.material.color = couleur;
            selfColor = couleur;
        }
    }

}
