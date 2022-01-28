using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPositionRuntime : MonoBehaviour
{
    public Vector3 localPosition;
    public Vector3 worldPosition;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        localPosition = transform.localPosition;
        worldPosition = transform.position;
    }
}
