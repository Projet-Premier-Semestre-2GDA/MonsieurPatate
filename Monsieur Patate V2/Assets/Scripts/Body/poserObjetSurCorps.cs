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
    //---------------------------Gestions des points d'attache---------------------------

    public GameObject[] listePointAttache = new GameObject[6];
    public string tagPointAttache = "pointAttache";
    //---------------------------Gestions des Membre qu'on peut ajouter---------------------------

    public string tagMembre = "membre";
    
    public List<GameObject> MembrePossible = new List<GameObject>(2);
    //Vector3[] pointPris;
    //---------------------------Gestions des Membre qui ont ete creer---------------------------

    public GameObject[] ObjetCreer = new GameObject[6];

    GameObject choosenOne;

    Color randomColor;


    
    void Start()
    {
        listePointAttache = GameObject.FindGameObjectsWithTag(tagPointAttache);
        randomColor = Random.ColorHSV();
        choosenOne = MembrePossible[0];
        ObjetCreer = new GameObject[6];
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
        Debug.Log("ChoosingGroup : " + choosingGroup);
        Debug.Log("ChoosingObject : " + choosingObject);

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

        TestSiDoubleGameObject(ObjetCreer);

        

        
        choosenOne = MembrePossible[IndexChoosenOne];

        //Debug.Log(choosenOne.name); //fonctionne

        //--------------------------------------Ajout d'un membre ou suppression d'un membre--------------------------------------
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

                if (hit.collider.CompareTag(this.gameObject.tag))
                {
                    //Debug.Log("Je fonctionne ?"); //fonctionne
                    int testTemp = 0;

                    for (int i = 0; i < ObjetCreer.Length; i++)
                    {
                        if (ObjetCreer[i] != null)
                        {
                            testTemp++;
                        }
                    }
                    Debug.Log(testTemp);


                    if (testTemp < 6)
                    {
                        //Debug.Log("Je fonctionne ?");//fonctionne
                        Transform PointChoosen = FindNearestPoint(hit.point, listePointAttache);

                        if (!CheckIfTaken(PointChoosen.position, ObjetCreer))
                        {
                            Debug.Log("J'ajoute un objet");
                            GameObject objectTemp = MettreLeMembreSurLeCorps(PointChoosen);

                            for (int i = 0; i < ObjetCreer.Length; i++)
                            {
                                if (ObjetCreer[i] == null)
                                {
                                    ObjetCreer.SetValue(objectTemp, i);
                                }

                            }


                        }
                    }

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
                if (hit.collider.tag == "membre")
                {
                    //Debug.Log("j'atteint ?");
                    for (int i = 0; i < ObjetCreer.Length; i++)
                    {
                        if (ObjetCreer[i] != null)
                        {
                            if (ObjetCreer[i] == hit.collider.transform.gameObject)
                            {
                                ObjetCreer.SetValue(null, i);
                                //Debug.LogError("coucou");
                                //break;
                            }
                        }

                    }
                    //Debug.Log("Je détruit l'objet là hein");
                    Destroy(hit.collider.gameObject);

                }
            }

        }
    }


    private GameObject MettreLeMembreSurLeCorps(Transform PointChoosen)
    {
        
        GameObject objectTemp = Instantiate(choosenOne, PointChoosen);
        Membre membreTemp = objectTemp.GetComponent<Membre>();
        membreTemp.groupeMembre = groupeSelectionner;
        GetComponent<ControlMembre>().AddMembre(membreTemp, groupeSelectionner);
        return objectTemp;
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

    bool CheckIfTaken(Vector3 objetDeTest, GameObject[] ListCheck)
    {
        bool test = false;

        foreach (var item in ListCheck)
        {
            if(item != null)
            {
                if (item.transform.position == objetDeTest)
                {
                    test = true;
                }
            }
            
        }

        return test;
    }
    bool TestSiDoubleGameObject(GameObject[] liste) 
    {
        bool test = false;
        for (int i = 0; i < liste.Length; i++)
        {
            for (int j = 0; j < liste.Length; j++)
            {
                if(i != j && liste[i] != null && liste[j] != null)
                {
                    if(liste[i] == liste[j])
                    {
                        test = true;
                        liste[j] = null;
                    }
                }
            }
        }
        return test;
    }

    

    void debugDeListe()
    {
        Debug.Log("-----------------------BEGIN_DEBUG-------------------------------");

        for (int i = 0; i < ObjetCreer.Length; i++)
        {

            if (ObjetCreer[i] == null)
            {
                Debug.Log("L'objet numéro " + i + " est null");
            }
            else
            {
                if (ObjetCreer[i].name.Contains("Bras"))
                {
                    Debug.Log("L'objet numéro " + i + " est un Bras");
                }
                else if (ObjetCreer[i].name.Contains("Jambe"))
                {
                    Debug.Log("L'objet numéro " + i + " est une Jambe");
                }
                else
                {
                    Debug.Log("L'objet numéro " + i + " est... je sais pas ce que c'est, c'est un \" " + ObjetCreer[i].name + "\"");
                }

            }

        }
        Debug.Log("-----------------------END_DEBUG-------------------------------");

    }

}
