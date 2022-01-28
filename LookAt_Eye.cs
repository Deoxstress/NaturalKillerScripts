using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt_Eye : MonoBehaviour
{
    public Transform target = null;

    void Start()
    {
        
    }

    void Update()
    {
        //transform.LookAt(target);

        Vector3 relativePos = target.position - transform.position;

        // the second argument, upwards, defaults to Vector3.up
        Quaternion rotation = Quaternion.LookRotation(relativePos, new Vector3(0, 1, 0));
        transform.rotation = rotation * Quaternion.Euler(90, 90, 90);
    }
}
