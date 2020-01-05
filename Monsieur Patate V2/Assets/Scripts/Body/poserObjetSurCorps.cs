using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poserObjetSurCorps : MonoBehaviour
{
    //---------------------------Choix du groupe et des objet---------------------------
    float choosingGroup;
    bool OnAxisGroupDown = false;
    bool OnAxisGroup = false;
    float choosingObject;
    bool OnAxisObjectDown = false;
    bool OnAxisObject = false;

    public int groupeSelectionner = 0;
    public int IndexChoosenOne;
    
    //---------------------------Gestions des Membre qu'on peut ajouter---------------------------

    public string tagMembre = "membre";
    
    public List<GameObject> MembrePossible = new List<GameObject>(2);
    //Vector3[] pointPris;
    //---------------------------Gestions des Membre qui ont ete creer---------------------------

    //public GameObject[] ObjetCreer = new GameObject[6];
    
    GameObject membreChoisi;

    //Color randomColor;


    
    void Start()
    {
        
        //randomColor = Random.ColorHSV();
        membreChoisi = MembrePossible[0];
    }

    // Update is called once per frame
    void Update()
    {
        //--------------------------------------PreGestionDesInputAxis--------------------------------------
        if (Input.GetAxis("ChooseGroupe") !=0)
        {
            if (!OnAxisGroup)
            {
                OnAxisGroupDown = true;
            }
            else
            {
                OnAxisGroupDown = false;
            }
            OnAxisGroup = true;
        }
        else
        {
            OnAxisGroup = false;
            OnAxisGroupDown = false;
        }

        if (Input.GetAxis("ChooseObject") != 0)
        {
            if (!OnAxisObject)
            {
                OnAxisObjectDown = true;
            }
            else
            {
                OnAxisObjectDown = false;
            }
            OnAxisObject = true;
        }
        else
        {
            OnAxisObject = false;
            OnAxisObjectDown = false;
        }
        //--------------------------------------Gestion des Membre--------------------------------------
        if (OnAxisGroupDown) choosingGroup = Input.GetAxis("ChooseGroupe");  else choosingGroup = 0;

        if (OnAxisObjectDown) choosingObject = Input.GetAxis("ChooseObject"); else choosingObject = 0;
        //En theorie ca marche mais ma manette fais de la merde donc je peux pas tester
        //Debug.Log("ChoosingGroup : " + choosingGroup);
        //Debug.Log("ChoosingObject : " + choosingObject);

        //Choix du membre et groupe

        IndexChoosenOne += Mathf.RoundToInt(choosingObject);
        groupeSelectionner += Mathf.RoundToInt(choosingGroup);
        if (IndexChoosenOne < 0)
        {
            IndexChoosenOne = MembrePossible.Count - 1;
        }
        else if (IndexChoosenOne >= MembrePossible.Count)
        {
            IndexChoosenOne = 0;
        }
        if (groupeSelectionner > 1)
        {
            groupeSelectionner = 0;
        }
        else if (groupeSelectionner < 0)
        {
            groupeSelectionner = 1;
        }


        

        
        membreChoisi = MembrePossible[IndexChoosenOne];

        //Debug.Log(choosenOne.name); //fonctionne

        //--------------------------------------Ajout d'un membre ou suppression d'un membre--------------------------------------
        if (PauseScript.gameIsPause)
        {
            if (Input.GetButtonDown("AjouterMembre"))
            {
                //Debug.Log("Bouton AjouterMembre Appuyer"); //Fonctionne
                //debugDeListe();
                Ray ray = Camera.main.ScreenPointToRay(ControlCurseur.positionCurseur);
                Debug.DrawRay(ray.origin, ray.direction * 50, Color.white);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 50f))
                {
                    //Debug.DrawLine(ray.origin, hit.point,randomColor,5f);
                    Debug.Log(hit.collider.tag); //fonctionne
                                                 //Debug.Log("L'objet c'est " + );

                    if (hit.collider.CompareTag("Player") || hit.collider.CompareTag(tagMembre))
                    {
                        MettreLeMembreSurLeCorps(hit.point, hit.collider.gameObject);

                    }

                }


                //debugDeListe();
            }
            if (Input.GetButtonDown("EnleverMembre"))
            {
                Debug.Log("Bouton EnleverMembre Appuyer"); //Fonctionne
                                                           //debugDeListe();
                Ray ray = Camera.main.ScreenPointToRay(ControlCurseur.positionCurseur);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 50f))
                {
                    //Debug.DrawLine(ray.origin, hit.point,randomColor,5f);
                    Debug.Log(hit.collider.tag); //fonctionne
                                                 //Debug.Log("L'objet c'est " + );
                    if (hit.collider.CompareTag("membre"))
                    {
                        hit.collider.GetComponent<PointAttache>().SupprimerObjet();
                        //Debug.Log(pointAttache.objetCreer);
                        //for (int i = 0; i < pointAttache.objetCreer.Length; i++)
                        //{
                        //    if (pointAttache.objetCreer[i] != null)
                        //    {
                        //        if (pointAttache.objetCreer[i] == hit.collider.transform.gameObject)
                        //        {
                        //            pointAttache.objetCreer.SetValue(null, i);
                        //            //Debug.LogError("coucou");
                        //            //break;
                        //        }
                        //    }

                        //}
                        //removeObjetFromControlMembre(hit.collider.gameObject);
                        ////Debug.Log("Je détruit l'objet là hein");
                        //Destroy(hit.collider.gameObject);

                    }
                }

            }
        }

    }
    private void RemoveObjetFromControlMembre(GameObject objectTemp)
    {
        //Membre leMembreEnQuestion = objectTemp.GetComponent<Membre>();
        GetComponent<ControlMembre>().RemoveMembre(objectTemp.GetComponent<Membre>());
    }

    private void MettreLeMembreSurLeCorps(Vector3 pointChoose, GameObject membreParent)
    {
        PointAttache pointAttacheParent = membreParent.GetComponent<PointAttache>();
        Transform transformChoose = FindNearestPoint(pointChoose, pointAttacheParent.listePointAttache);
        //creation du membre
        GameObject membreEnfant = Instantiate(membreChoisi, transformChoose);
        //Assignation du groupe
        Membre scriptMembreEnfant = membreEnfant.GetComponent<Membre>();
        scriptMembreEnfant.groupeMembre = groupeSelectionner;
        
        //Assignation des parents
        PointAttache pointAttacheObjet = membreEnfant.GetComponent<PointAttache>();
        pointAttacheObjet.parent = membreParent;
        //Assignation des enfants
        pointAttacheParent.SetEnfant(transformChoose, membreEnfant);
        //Control du membre
        GetComponent<ControlMembre>().AddMembre(scriptMembreEnfant, groupeSelectionner);
        scriptMembreEnfant.rbParent = membreParent.GetComponent<Rigidbody>();
    }

    Transform FindNearestPoint(Vector3 pointCompare, GameObject[] arrayTest)
    {
        Transform nearestPoint = transform;

        //Fonction qui cherche le point le plus pret
        for (int i = 0; i < arrayTest.Length; i++)
        {
            Vector3 PositionTemp = arrayTest[i].transform.position;

            if (nearestPoint == transform)
            {
                nearestPoint = arrayTest[i].transform;
            }
            else if (Mathf.Abs((PositionTemp - pointCompare).magnitude) < Mathf.Abs((nearestPoint.position - pointCompare).magnitude))
            {
                nearestPoint = arrayTest[i].transform;
            }
        }
        return nearestPoint;
    }


}
