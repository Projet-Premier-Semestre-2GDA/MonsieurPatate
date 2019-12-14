using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMembre : MonoBehaviour
{
    
    public List<Membre> GroupeMembreUn = new List<Membre>();
    public List<Membre> GroupeMembreDeux = new List<Membre>();
    Rigidbody rb;
    Color selfColor;

    Vector2 ActionGroupe;
    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {
        ActionGroupe = new Vector2(Input.GetAxis("ActionGroupe1"), Input.GetAxis("ActionGroupe2"));
        if (ActionGroupe.x != 0)
        {
            UtilisationGroupeMembre(GroupeMembreUn, ActionGroupe.x);

        }
        if (ActionGroupe.y != 0)
        {
            UtilisationGroupeMembre(GroupeMembreDeux, ActionGroupe.y);

        }

    }

    private void UtilisationGroupeMembre(List<Membre> listeDeMembre, float intensite = 1)
    {
        foreach (var item in listeDeMembre)
        {
            item.Action(intensite);
            Debug.Log("l'objet \"" + item.name + "\" a effectue une action d'intensité " + intensite);
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
        switch (groupeDAppartenance)
        {
            case 0:
                GroupeMembreUn.Add(membreAAjouter);
                break;
            case 1:
                GroupeMembreDeux.Add(membreAAjouter);
                break;
            default:
                AddMembre(
                    membreAAjouter,
                    Random.Range(0, 2));
                break;
        }
    }

    public void RemoveMembre(int groupe, Membre membreAEnlever)
    {
        if (groupe == 0)
        {
            GroupeMembreUn.Remove(membreAEnlever);
        }
        else if (groupe == 1)
        {
            GroupeMembreDeux.Remove(membreAEnlever);
        }
        else
        {
            Debug.Log("Le groupe " + groupe + " n'existe pas ou n'a pas ete creer.");
        }
    }

}
