using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityScript.Lang;

public class poserObjetSurCorps : MonoBehaviour
{
    //---------------------------Les couleurs en fonction du groupe---------------------------
    public Color[] groupeColor = new Color[4] { Color.blue, Color.red, Color.green, Color.magenta };

    //---------------------------Choix du groupe et des objet---------------------------
    public int chosedGroupIndex = 0;
    public int chosedLimbIndex = 0;

    //---------------------------Gestions des Membre qu'on peut ajouter---------------------------

    [SerializeField] private bool afficherLesMembresAuStart = true;
    public string limbTag = "membre";

    public List<GameObject> Limbs = new List<GameObject>(2);
    //Vector3[] pointPris;
    //---------------------------Gestions des Membre qui ont ete creer---------------------------

    //public GameObject[] ObjetCreer = new GameObject[6];
    
    [HideInInspector]public GameObject chosedLimb;

    private Color randomColor;

    public Ray ray;
    //----------------------------Références à l'UI'------------------------------------------------

    [SerializeField] private GameObject LeftSlot;
    private SlotScript leftSS;
    [SerializeField] private GameObject RightSlot;
    private SlotScript rightSS;
    [SerializeField] private GameObject UpSlot;
    private SlotScript upSS;
    [SerializeField] private GameObject DownSlot;
    private SlotScript downSS;
    [SerializeField] private GameObject CenterSlot;
    private CenterSlotScript centerSS;
    //----------------------------Variable pour le preview-----------------------------------------

    PreviewMembre preview;

    void Start()
    {
        preview = GetComponent<PreviewMembre>();
        if (afficherLesMembresAuStart)
        {
            if (Limbs.Count == 0)
            {
                Debug.LogError("Il n'y a pas de Membre dans " + this.name + ".");
                Debug.LogError("_______________________________________________________");

            }
            else
            {
                Debug.LogWarning("Il y a " + Limbs.Count + " membres dans " + this.name + ". Les voici :\n");
                for (int i = 0; i < Limbs.Count; ++i)
                {
                    Debug.LogWarning("Le numero " + (i + 1) + " est un " + Limbs[i].name);
                }
                Debug.LogWarning("_______________________________________________________");
            }
        }
        randomColor = Random.ColorHSV();
        this.chosedLimb = this.Limbs[0];
        
        //UI startup
        this.leftSS = this.LeftSlot.GetComponent<SlotScript>();
        this.rightSS = this.RightSlot.GetComponent<SlotScript>();
        this.downSS = this.DownSlot.GetComponent<SlotScript>();
        this.upSS = this.UpSlot.GetComponent<SlotScript>();
        this.centerSS = this.CenterSlot.GetComponent<CenterSlotScript>();
    }

    // Update is called once per frame
    void Update() {
        
        
        

        //--------------------------------------Ajout d'un membre ou suppression d'un membre--------------------------------------
        if (PauseScript.isGamePaused) {
            //Sélection des membres et groupes
            ChooseLimb();
            ChooseGroup();
            AddLimb();
            RemoveLimb();
        }
    }

    private void ChooseLimb() {
        if (OoskaCustom.GetAxisDown("ChooseLimb")) {
            this.chosedLimbIndex += Mathf.RoundToInt(Input.GetAxisRaw("ChooseLimb"));

            int limbLeftIndex;
            int limbRightIndex;

            chosedLimbIndex = ClampIndexInArray(this.chosedLimbIndex, this.Limbs.Count);
            
            limbLeftIndex = ClampIndexInArray(this.chosedLimbIndex - 1, this.Limbs.Count);
            limbRightIndex = ClampIndexInArray(this.chosedLimbIndex + 1, this.Limbs.Count);
            
            this.chosedLimb = this.Limbs[this.chosedLimbIndex];
            Debug.Log(chosedLimb.name);
            
            //UI Stuff
            //Increment/decrement on the image array
            this.leftSS.UpdateIcon(limbLeftIndex);
            this.centerSS.UpdateIcon(this.chosedLimbIndex);
            this.rightSS.UpdateIcon(limbRightIndex);
        }
    }

    private int ClampIndexInArray(int index, int arrayLength) {
        if (index < 0){
            index = arrayLength - 1;
        }
        else if (index >= arrayLength){
            index= 0;
        }
        return index;
    }


    private void ChooseGroup() {
        if (OoskaCustom.GetAxisDown("ChooseGroup")) {
            this.chosedGroupIndex += Mathf.RoundToInt(Input.GetAxisRaw("ChooseGroup"));
            this.chosedGroupIndex = ClampIndexInArray(chosedGroupIndex, ControlMembre.numberOfLimb);
            
            int groupDownIndex = ClampIndexInArray(this.chosedGroupIndex - 1, ControlMembre.numberOfLimb);
            int groupUpIndex = ClampIndexInArray(this.chosedGroupIndex + 1, ControlMembre.numberOfLimb);
            
            //UI Stuff
            //Increment/decrement on the image array
            
            this.downSS.UpdateIcon(groupDownIndex);
            this.centerSS.UpdateIconGroup(this.chosedGroupIndex);
            this.upSS.UpdateIcon(groupUpIndex);
        }
    }
    private void AddLimb() {
        ray = Camera.main.ScreenPointToRay(ControlCurseur.positionCurseur);

        if (Input.GetButtonDown("AddLimb")) {


            Debug.DrawRay(ray.origin, ray.direction * 50, Color.white);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 50f,layerMask: LayerMask.GetMask("BodyLayer")))
            {
                //Debug.DrawLine(ray.origin, hit.point,randomColor,5f);
                //Debug.Log(hit.collider.attachedRigidbody.tag); //fonctionne
                //Debug.Log("L'objet c'est " + );
        
                if (hit.collider.attachedRigidbody.CompareTag("Player") || hit.collider.attachedRigidbody.CompareTag("membre"))
                {
                    this.PutLimb(hit.point, hit.collider.attachedRigidbody.gameObject);
                }
            }
            
            //debugDeListe();
        }
    }
    
    private void RemoveLimb() {
        if (Input.GetButtonDown("RemoveLimb"))
        {
            

            Ray ray = Camera.main.ScreenPointToRay(ControlCurseur.positionCurseur);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 50f,layerMask: LayerMask.GetMask("BodyLayer")))
            {
                //Debug.DrawLine(ray.origin, hit.point,randomColor,5f);
                Debug.Log(hit.collider.attachedRigidbody.tag); //fonctionne
                //Debug.Log("L'objet c'est " + );
                if (hit.collider.attachedRigidbody.tag == "membre") {
                    hit.collider.attachedRigidbody.GetComponent<PointAttache>().SupprimerObjet();
        
                }
            }
        
        }
    }

    private void PutLimb(Vector3 pointChoose, GameObject membreParent)
    {
        PointAttache pointAttacheParent = membreParent.GetComponent<PointAttache>();
        Transform transformChoose = FindNearestPoint(pointChoose, pointAttacheParent.listePointAttache);
        //creation du membre
        GameObject membreEnfant = Instantiate(this.chosedLimb, transformChoose);
        //Assignation du groupe
        Membre scriptMembreEnfant = membreEnfant.GetComponent<Membre>();
        scriptMembreEnfant.groupeMembre = this.chosedGroupIndex;
        scriptMembreEnfant.SetMembreColor(groupeColor[this.chosedGroupIndex]);
        //Assignation des parents
        PointAttache pointAttacheObjet = membreEnfant.GetComponent<PointAttache>();
        pointAttacheObjet.parent = membreParent;
        //Assignation des enfants
        pointAttacheParent.SetEnfant(transformChoose, membreEnfant);
        //Control du membre
        GetComponent<ControlMembre>().AddMembre(scriptMembreEnfant, this.chosedGroupIndex);
        scriptMembreEnfant.rbParent = membreParent.GetComponent<Rigidbody>();
    }

    public Transform FindNearestPoint(Vector3 pointCompare, GameObject[] arrayTest)
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
