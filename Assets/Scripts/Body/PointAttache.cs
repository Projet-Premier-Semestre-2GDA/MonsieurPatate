﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAttache : MonoBehaviour
{
    [SerializeField] private bool automaticSearch = true;
    public GameObject[] listePointAttache = new GameObject[6];
    public GameObject[] objetCreer = new GameObject[6];
    public Rigidbody[] attachRigidBody = new Rigidbody[6];
    [HideInInspector]public GameObject parent;
    ControlMembre controlMembre;
    public string tagPointAttache = "pointAttache";

    private void Start()
    {
        if (automaticSearch)
        {
            SearchAttachPoint();
        }
        name += Random.value;
        //Debug.LogError("We find the  " + GameObject.FindGameObjectWithTag("Player"));
        controlMembre = GameObject.FindGameObjectWithTag("Player").GetComponent<ControlMembre>();
        objetCreer = new GameObject[listePointAttache.Length];
    }

    private void SearchAttachPoint()
    {
        FindAttachPoint[] attachPointList = GetComponentsInChildren<FindAttachPoint>();
        listePointAttache = new GameObject[attachPointList.Length];
        attachRigidBody = new Rigidbody[attachPointList.Length];
        {
            for (int i = 0; i < attachPointList.Length; i++)
            {
                listePointAttache[i] = attachPointList[i].gameObject;
                if (attachPointList[i].attachRigibody != null)
                {
                    attachRigidBody[i] = attachPointList[i].attachRigibody;
                }
                else
                {
                    attachRigidBody[i] = GetComponent<Rigidbody>();
                }
            }
        }
    }

    private void LateUpdate()
    {
        TestSiDoubleGameObject(objetCreer);
    }

    bool TestSiDoubleGameObject(GameObject[] liste)
    {
        bool test = false;
        for (int i = 0; i < liste.Length; i++)
        {
            for (int j = 0; j < liste.Length; j++)
            {
                if (i != j && liste[i] != null && liste[j] != null)
                {
                    if (liste[i] == liste[j])
                    {
                        test = true;
                        liste[j] = null;
                    }
                }
            }
        }
        return test;
    }

    public bool SetEnfant(Transform transformCible, GameObject enfant)
    {
        
        int indexChoosen = -1;
        for (int i = 0; i < listePointAttache.Length; i++)
        {
            if (transformCible == listePointAttache[i].transform)
            {
                indexChoosen = i;
                break;
            }
        }
        if (indexChoosen < 0)
        {
            Debug.Log("Il y a un probleme quelque part on a pas trouver de le numero dans la liste");
            return false;
        }
        else if (objetCreer[indexChoosen] != null)
        {
            Debug.Log("le point est deja pris");
            Destroy(enfant);
            return false;
        }
        else
        {
            objetCreer[indexChoosen] = enfant;
            enfant.transform.parent = transformCible;
            enfant.transform.position = transformCible.position;
            enfant.transform.rotation = transformCible.rotation;
            enfant.GetComponent<Membre>().rbParent = attachRigidBody[indexChoosen];
        }

            return true;
    }

    public void SupprimerObjet(bool parentDejaPrevenu = false/*, ControlMembre controlMembre = null*/)
    {
        
        FMODUnity.RuntimeManager.PlayOneShot("event:/Enlever Membre");

        if (!parentDejaPrevenu) //permet de se supprimer des parents si cela n'a pas deja ete fait
        {
            parent.GetComponent<PointAttache>().SupprimerEnfant(gameObject);
        }
        foreach (var item in objetCreer)//permet de prevenir tout les enfants de se supprimer
        {
            if (item != null)
            {
                item.GetComponent<PointAttache>().SupprimerObjet(true);
            }
        }
        //et la c'est la vrai partie ou on se supprime avec tout ce qui rentre en compte
        Membre thisMemebre = this.gameObject.GetComponent<Membre>();
        //Debug.LogError(controlMembre);
        controlMembre.RemoveMembre(thisMemebre);
        Destroy(this.gameObject);
    }
    public void SupprimerEnfant(GameObject enfant)
    {
        
        objetCreer.SetValue(
            null,
            GetIndex(enfant, objetCreer));
    }

    int GetIndex(GameObject objectAsk, GameObject[] array)
    {
        int index = -1;
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == objectAsk)
            {
                index = i;
                break;
            }
        }

        if (index == -1) Debug.Log("L'objet demande n'est pas dans le tableau");

        return index;
    }

}
