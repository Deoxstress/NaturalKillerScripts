using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBossMissile : MonoBehaviour
{
    public Transform missileTarget;
    public Rigidbody missileRb;

    [SerializeField]
    private float turnSpeed, missileFlySpeed, knockBackStrength;
    
    private Transform missileTransform;
    void Start()
    {
        missileTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        missileRb.velocity = missileTransform.forward * missileFlySpeed;

        Quaternion missileTargetRot = Quaternion.LookRotation(missileTarget.position - missileTransform.position);

        missileRb.MoveRotation(Quaternion.RotateTowards(missileTransform.rotation, missileTargetRot, turnSpeed));
    }

    private void OnCollisionEnter(Collision collision)
    {
        /*
        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerMovement>().invulframes <= 0)
        {          
            Destroy(this.gameObject);
        }
        */
    }
}
