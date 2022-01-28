using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutoutCrystal : MonoBehaviour
{
    [SerializeField]
    private Transform targetObject;

    [SerializeField]
    private LayerMask wallMask;

    private Camera mainCamera;
    private RaycastHit hit;


    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    private void Update()
    {

        /*if (Physics.Raycast(transform.position, transform.forward, out hit, 20f, wallMask))
        {
            if (hit.collider)
            {
                hit.collider.GetComponent<Renderer>().enabled = false;
                hit.collider.GetComponent<HideMonolith>().timerToShowMonolith = 0.4f;
                hit.collider.GetComponent<HideMonolith>().isHidden = true;

            }

        }
        if (Physics.Raycast(transform.position, -transform.forward, out hit, 20f, wallMask))
        {
            if (hit.collider)
            {
                hit.collider.GetComponent<Renderer>().enabled = false;
                hit.collider.GetComponent<HideMonolith>().timerToShowMonolith = 0.4f;
                hit.collider.GetComponent<HideMonolith>().isHidden = true;

            }

        }*/


        Vector2 cutoutPos = mainCamera.WorldToViewportPoint(targetObject.position);
        cutoutPos.y /= (Screen.width / Screen.height);

        Vector3 offset = targetObject.position - transform.position;
        RaycastHit[] hitObjects = Physics.RaycastAll(transform.position, offset, offset.magnitude, wallMask);

        for (int i = 0; i < hitObjects.Length; ++i)
        {
            Material[] materials = hitObjects[i].transform.GetComponent<Renderer>().materials;

            for(int m = 0; m < materials.Length; ++m)
            {
                materials[m].SetVector("_CutoutPos", cutoutPos);
                materials[m].SetFloat("_CutoutSize", 0.1f);
                materials[m].SetFloat("_FalloffSize", 0.05f);
            }
        }
        
    }
}

