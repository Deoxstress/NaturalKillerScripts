using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour, IPooledObject
{
    [SerializeField] private float lifeTime;
    [SerializeField] private float speed;
    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private bool playerIsClose;
    public float sideForce = 0.1f;
    public float upForce = 1.0f;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        transform.LookAt(player);

        if(lifeTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }


    void FixedUpdate()
    {
        if(playerIsClose)
            PickUpMoveTowardsPlayer();
        //transform.LookAt(player, player.up);
    }

    void PickUpMoveTowardsPlayer()
    {
        Vector3 movement = Vector3.forward.normalized * speed;
        rb.MovePosition(rb.position + transform.TransformDirection(movement) * Time.fixedDeltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            playerIsClose = true;
        }
    }

    /*void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerIsClose = false;
        }
    }*/

    public void OnObjectSpawn()
    {
        float xForce = Random.Range(-sideForce, sideForce);
        float yForce = Random.Range(upForce /2f, upForce);
        float zForce = Random.Range(-sideForce, sideForce);

        Vector3 force = new Vector3(xForce, yForce, zForce);
        GetComponent<Rigidbody>().velocity = force;
        
    }
}
