using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class GravityBody : MonoBehaviour
{

    private GravityAttractor virusLayer;
    [SerializeField] public bool isFlying, isFalling;
    public float gravityForFalling;
    Rigidbody rb;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        virusLayer = GameObject.FindGameObjectWithTag("VirusLayer").GetComponent<GravityAttractor>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isFlying && !isFalling)
        {
            virusLayer.AttractBody(transform);
        }
        if(isFalling)
        {
            virusLayer.AttractBodyForcefully(transform, gravityForFalling);
        }
        if(isFlying)
        {
            virusLayer.OnlyRotateBody(transform);
        }       
    }

    public void SetIsFlying(bool newValue)
    {
        isFlying = newValue;
    }
}
