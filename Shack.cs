using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shack : MonoBehaviour
{
    private Vector3 startingPos;
    [SerializeField] private float speed;
    [SerializeField] private float amount;

    void Awake()
    {
        startingPos.x = transform.localPosition.x;
        startingPos.y = transform.localPosition.y;
        startingPos.z = transform.localPosition.z;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(startingPos.x * (Mathf.Sin(Time.deltaTime * speed) * amount), startingPos.y * (Mathf.Sin(Time.deltaTime * speed) * amount), startingPos.z);
    }
}
