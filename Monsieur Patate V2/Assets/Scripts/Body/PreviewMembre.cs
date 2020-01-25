using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewMembre : MonoBehaviour
{
    /*[SerializeField]*/ private poserObjetSurCorps poseur;
    [SerializeField] private Material materialPreview;
    private GameObject objectSelected;
    [SerializeField] private float tauxDeTransparence = 0.6f;
    public string tagPreview = "membrePreview";
    [HideInInspector] public GameObject previewObject;
    private bool hasChangeOfMembre = true;
    private bool hasChangeOfMat = true;
    private void Start()
    {
        poseur = GetComponent<poserObjetSurCorps>();
        
    }
    private void Update()
    {
        if (objectSelected == null)
        {
            objectSelected = poseur.chosedLimb;
        }
        else
        {
            if (objectSelected.name != poseur.chosedLimb.name)
            {
                objectSelected = poseur.chosedLimb;
                hasChangeOfMembre = true;
            }
            else
            {
                hasChangeOfMembre = false;
            }

        }
        if (PauseScript.isGamePaused)
        {
            
            ChangeColorMat(poseur.groupeColor[poseur.chosedGroupIndex]);
            
            Debug.Log("The game is Pause");
            if (true)
            {
                
                Debug.Log("PreviewMembre start a raycast");
                Ray ray = poseur.ray;
                Debug.DrawRay(ray.origin, ray.direction * 50, Color.white);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 50f, layerMask : LayerMask.GetMask("BodyLayer")))
                {
                    Debug.Log("PreviewMembre hit something " + hit.collider.tag);

                    if (hit.rigidbody != null)
                    {
                        if (hit.collider.attachedRigidbody.CompareTag("Player") || hit.collider.attachedRigidbody.CompareTag("membre"))
                        {
                            Debug.Log("PreviewMembre hit " + hit.rigidbody.name);
                            CreationPreview(hit.point, hit.collider.attachedRigidbody.gameObject);
                        }
                        else if (!hit.rigidbody.CompareTag(tagPreview))
                        {
                            Debug.Log(hit.rigidbody.tag);
                            DestroyPreview();
                        }
                    }
                    else
                    {
                        Debug.Log("Preview membre hit " + hit.collider.tag);
                        DestroyPreview();
                    }
                    
                }
                else
                {
                    DestroyPreview();
                }

            }
        }
        else
        {
            DestroyPreview();
        }
    }

    private void CreationPreview(Vector3 pointTouch, GameObject membreParent)
    {
        PointAttache pointAttacheParent = membreParent.GetComponent<PointAttache>();
        Transform transformChoose = poseur.FindNearestPoint(pointTouch, pointAttacheParent.listePointAttache);
        if (previewObject != null)
        {
            if (hasChangeOfMembre)
            {
                Destroy(previewObject);
                CreateObjectPreview(transformChoose);
            }
            else
            {
                previewObject.transform.position = transformChoose.position;
                previewObject.transform.rotation = transformChoose.rotation;
            }
        }
        else
        {
            CreateObjectPreview(transformChoose);
        }

    }
    private void ChangeColorMat(Color colorChoose)
    {
        colorChoose.a = tauxDeTransparence;
        materialPreview.color = colorChoose;
    }
    private void CreateObjectPreview(Transform theTransform)
    {
        previewObject = Instantiate(objectSelected, theTransform.position, theTransform.rotation);
        foreach (var item in previewObject.GetComponentsInChildren<MeshRenderer>(false))
        {
            item.material = materialPreview;
        }
        foreach (var item in previewObject.GetComponentsInChildren<Collider>())
        {
            item.tag = tagPreview;
            item.gameObject.layer = 14; //layer "Preview"
        }
        previewObject.layer = 14; //layer "Preview"
        previewObject.tag = tagPreview;
    }
    private void DestroyPreview()
    {
        if (previewObject != null)
        {
            Destroy(previewObject);
        }
    }

}
