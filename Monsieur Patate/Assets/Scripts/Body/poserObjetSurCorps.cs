using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poserObjetSurCorps : MonoBehaviour
{
    //Il faut encore faire un peu de deubug parce que j'incrémente une variable dans le vide
    // Start is called before the first frame update
    public GameObject[] listePointAttache = new GameObject[6];
    public string tagPointAttache = "pointAttache";

    public string tagMembre = "membre";

    public List<GameObject> MembrePossible = new List<GameObject>(2);
    //Vector3[] pointPris;
    public GameObject[] ObjetCreer = new GameObject[6];

    GameObject choosenOne;
    private int IndexChoosenOne;

    Color randomColor;

    private KeyCode lastKeyCode;

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
        
        TestSiDoubleGameObject(ObjetCreer);

        //Choix du membre

        switch (lastKeyCode)
        {
            case KeyCode.Keypad0:
                IndexChoosenOne = 0;
                break;
            case KeyCode.Keypad1:
                IndexChoosenOne = 1;
                break;
            default:
                break;
        }
        choosenOne = MembrePossible[IndexChoosenOne];

        //Debug.Log(choosenOne.name); //fonctionne

        //Ajout d'un membre ou suppression d'un membre
        if (Input.GetButtonDown("Fire1"))
        {
            //Debug.Log("Bouton Appuyer"); //Fonctionne
            //debugDeListe();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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
                            GameObject objectTemp = Instantiate(choosenOne, PointChoosen);

                            for (int i = 0; i < ObjetCreer.Length; i++)
                            {
                                if (ObjetCreer[i] == null)
                                {
                                    objectTemp.GetComponent<ControlMembre>().numeroMembre = i + 1;
                                    ObjetCreer[i] = objectTemp;
                                    break;
                                }
                            }
                        }
                        
                        
                    }
                    

                }
                else if (hit.collider.tag == "membre")
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

            //debugDeListe();
        }
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


    void OnGUI()
    {
        Event current = Event.current;
        while (Event.PopEvent(current))
        {
            if (!current.isKey) 
            { 
                return; 
            }

            KeyCode tempCode = current.keyCode;
            if (tempCode == KeyCode.None) 
            { 
                return; 
            }

            lastKeyCode = tempCode;
            //Debug.Log(current); //Fonctionne 
        }
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
