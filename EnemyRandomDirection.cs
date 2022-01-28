using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomDirection : MonoBehaviour
{
    [SerializeField] private Vector3 movementDir;
    [SerializeField] private Vector3 targetMoveAmount;
    [SerializeField] private Vector3 moveAmount;
    [SerializeField] private Vector3 smoothMoveVelocity;
    [SerializeField] private int speed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float cooldownToMove;
    [SerializeField] private float initCooldownToMove; // Time before choosing a new direction
    [SerializeField] private float initTimerToMove; // Time to move
    [SerializeField] private float timerToMove;
    [SerializeField] private bool isMoving;
    [SerializeField] private bool chooseNewDirection;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        cooldownToMove = initCooldownToMove;
        timerToMove = initTimerToMove;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(cooldownToMove <= 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
            cooldownToMove -= Time.deltaTime;
            chooseNewDirection = true;
            timerToMove = initTimerToMove;
        }
        if(isMoving)
        {
            timerToMove -= Time.deltaTime;
        }
        if(timerToMove <= 0)
        {
            isMoving = false;
            cooldownToMove = initCooldownToMove;
        }
    }

    void FixedUpdate()
    {
        
        
        if(isMoving && timerToMove >= 0)
        {
            if (chooseNewDirection)
            {
                movementDir = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f)).normalized;
                chooseNewDirection = false;
            }
            MoveInRandomDirection(movementDir);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
        
    }

    void MoveInRandomDirection(Vector3 randomMovementDir)
    {       
        targetMoveAmount = randomMovementDir * speed;
        moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, 0.05f);
        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);

    }
}
