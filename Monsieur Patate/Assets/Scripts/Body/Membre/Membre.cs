using UnityEngine;

public class Membre : MonoBehaviour
{
    // ceci est une nouvelle classe pour mes membre
    public Rigidbody rb;
    public Vector3 directionForce = new Vector3();
    public float vitesseDeRotation = 200;
    public virtual void Start()
    {
        Debug.Log("j'existe");
        rb = GetComponentInParent<Rigidbody>();
    }
    
    public virtual void FirstAction() //corresponds a la première touche d'action, généralement un mouvement
    {
        Debug.Log(transform.name + " make the first mouvement");
        transform.Rotate(Vector3.right, vitesseDeRotation * Time.deltaTime);
    }
    public virtual void SecondAction() //corresponds a la deuxieme touche d'action, généralement un mouvement
    {
        Debug.Log(transform.name + " make the second mouvement");
        transform.Rotate(Vector3.right, -vitesseDeRotation * Time.deltaTime);
    }
    public virtual void ThirdAction() //corresponds a la troisieme touche d'action, généralement une action contextuelle
    {
        Debug.Log(transform.name + " make an action");
        directionForce = -transform.up;
        rb.AddForce(directionForce * 10);
    }

}
