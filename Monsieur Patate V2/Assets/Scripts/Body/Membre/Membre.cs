using UnityEngine;

public class Membre : MonoBehaviour
{
    // ceci est une nouvelle classe pour mes membre
    public int groupeMembre = -1;
    public Color[] groupeColor = new Color[2] {Color.blue,Color.red};

    public Rigidbody rb;
    public Vector3 directionForce = new Vector3();
    public float vitesseDeRotation = 200;
    public virtual void Start()
    {
        Debug.Log("j'existe");
        rb = GetComponentInParent<Rigidbody>();
        SetMembreColor(groupeColor[groupeMembre]);
    }

    public virtual void Action(float analogiqueReturn = 1) //corresponds a la première touche d'action, généralement un mouvement
    {
        Debug.Log(transform.name + " make the first action with an intensity of " + analogiqueReturn);
    }

    protected void SetMembreColor(Color colorToSet)
    {
        foreach (var item in GetComponentsInChildren<MeshRenderer>())
        {
            foreach (var anotherItem in item.materials)
            {
                anotherItem.color = colorToSet;
            }
        }
        
    }

}
