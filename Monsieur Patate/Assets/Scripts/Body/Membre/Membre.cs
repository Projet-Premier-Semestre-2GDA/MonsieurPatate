using UnityEngine;

public class Membre : MonoBehaviour
{
    // ceci est une nouvelle classe pour mes membre
    //public Rigidbody rb;
    public Vector3 directionForce = new Vector3();
    public float vitesseDeRotation = 10;
    public virtual void Start()
    {
        Debug.Log("j'existe");
        //rb = GetComponent<Rigidbody>();
    }
    
    public virtual void FirstAction(Transform objet, Rigidbody rb) //corresponds a la première touche d'action, généralement un mouvement
    {
        Debug.Log(transform.name + " make the first mouvement");
    }
    public virtual void SecondAction(Transform objet, Rigidbody rb) //corresponds a la deuxieme touche d'action, généralement un mouvement
    {
        Debug.Log(transform.name + " make the second mouvement");
    }
    public virtual void ThirdAction(Transform objet, Rigidbody rb) //corresponds a la troisieme touche d'action, généralement une action contextuelle
    {
        Debug.Log(transform.name + " make an action");
    }

}
