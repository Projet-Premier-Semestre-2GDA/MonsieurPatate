using UnityEngine;

public class Membre : MonoBehaviour
{
    public string pathToSound = "event:/Membre/";
    // ceci est une nouvelle classe pour mes membre
    [HideInInspector]public int groupeMembre = -1;
    //public ForceMode typeDeForceAppliquee = ForceMode.Force;
    //public static Color[] groupeColor = new Color[4] {Color.blue,Color.red,Color.green,Color.magenta};
    protected FixedJoint joint;
    protected Rigidbody rb;
    protected Rigidbody rbPlayer;
    protected Rigidbody rbActive;
    [HideInInspector] public Rigidbody rbParent;
    protected Vector3 directionForce = new Vector3();
    //public float vitesseDeRotation = 200;
    public virtual void Start()
    {
        
        //Debug.Log("j'existe");
        //rb = GetComponentInParent<Rigidbody>();
        rb = GetComponent<Rigidbody>();
        joint = GetComponent<FixedJoint>();
        rbPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        //SetMembreColor(groupeColor[groupeMembre]);
        joint.connectedBody = rbParent;
        
    }

    public virtual void Action(float analogiqueReturn = 1) //corresponds a la première touche d'action, généralement un mouvement
    {
        //Debug.Log(transform.name + " of the group " + groupeMembre + " make the first action with an intensity of " + analogiqueReturn);
        if (Time.timeScale == 0)
        {
            analogiqueReturn = 0;
        }
        FMODUnity.RuntimeManager.PlayOneShot(pathToSound);
    }
    public virtual void NonAction()
    {
        //Debug.Log(this.name + " undo an action");
    }

    public void SetMembreColor(Color colorToSet)
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
