using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristalShardBehaviour : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] public bool isGrounded;
    private Vector3 MovementDir;
    [HideInInspector] public Vector3 playerPos;
    [SerializeField] private GameObject dustVFX;
    public Rigidbody rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isGrounded)
        {
            //transform.position += (playerPos - transform.position).normalized * speed * Time.deltaTime;

            transform.Translate(-transform.up * speed * Time.deltaTime, Space.World);
            transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
        }
        else
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
            
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            GameObject dustClone = Instantiate(dustVFX, transform.position, Quaternion.identity);
            dustClone.transform.localScale = new Vector3(3.25f, 3.25f, 3.25f);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        
    }
}
