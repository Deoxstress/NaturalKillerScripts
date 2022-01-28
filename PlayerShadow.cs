using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShadow : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private Rigidbody rb;
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.LookAt(player);
        MoveTowardsPlayer();
    }

    void MoveTowardsPlayer()
    {
        Vector3 movement = Vector3.forward.normalized * speed;
        rb.MovePosition(rb.position + transform.TransformDirection(movement) * Time.fixedDeltaTime);
    }
}
