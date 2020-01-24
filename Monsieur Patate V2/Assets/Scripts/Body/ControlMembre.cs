using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMembre : MonoBehaviour
{

    public static int numberOfLimb = 4;
    public List<Membre>[] arrayGroupLimb ;
    Rigidbody rb;
    Color selfColor;


    private void Awake()
    {
        arrayGroupLimb = new List<Membre>[numberOfLimb];
        for (int i = 0; i < arrayGroupLimb.Length; i++)
        {
            arrayGroupLimb[i] = new List<Membre>();
        }
    }

    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {
        //Utilisation des membre en fonction de si l'on relache les bouton ou non
        if (!PauseScript.isGamePaused)
        {
            for (int i = 0; i < numberOfLimb; i++)
            {

                if (UseButtonDown("ActionGroupe" + (i + 1)))
                {
                    UtilisationGroupeMembre(i);
                }
                if (UseButtonUp("ActionGroupe" + (i + 1)))
                {
                    NonUtilisationGroupeMembre(i);
                }
            }
        }
        

        

    }

    private bool UseButtonDown(string inputName)
    {
        if (inputName.Contains("1") || inputName.Contains("2"))
        {
            return OoskaCustom.GetAxisDown(inputName);
        }
        else
        {
            return Input.GetButtonDown(inputName);
        }
    }
    private bool UseButtonUp(string inputName)
    {
        if (inputName.Contains("1") || inputName.Contains("2"))
        {
            return OoskaCustom.GetAxisUp(inputName);
        }
        else
        {
            return Input.GetButtonUp(inputName);
        }
    }

    private void UtilisationGroupeMembre(int indexListeDeMembre, float intensite = 1)
    {
        foreach (var item in arrayGroupLimb[indexListeDeMembre])
        {
            if (item!= null)
            {
                item.Action(intensite);
            }

        }
    }
    private void NonUtilisationGroupeMembre(int indexListeDeMembre)
    {
        foreach (var item in arrayGroupLimb[indexListeDeMembre])
        {
            if (item != null)
            {
                item.NonAction();
            }

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

    public void AddMembre(Membre membreAAjouter,int groupeDAppartenance)
    {
        arrayGroupLimb[groupeDAppartenance].Add(membreAAjouter);
    }

    public void RemoveMembre(Membre membreAEnlever)
    {
        int groupe = membreAEnlever.groupeMembre;
        arrayGroupLimb[groupe].Remove(membreAEnlever);
    }

}
