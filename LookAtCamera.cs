using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LookAtCamera : MonoBehaviour
{

    [SerializeField] private Transform mainCamera;
    [SerializeField] private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        target.transform.LookAt(mainCamera, mainCamera.up);
    }
}
