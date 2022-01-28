using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBossController : MonoBehaviour
{

    [SerializeField] private Transform[] waypointArray;
    private int currentWaypointIndex = 0;
    [SerializeField] private float bossSpeed, rotateSpeed;
    [SerializeField] private GameObject projectile;
    [SerializeField] private int bossPhase;
    


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        
        transform.position = Vector3.MoveTowards(transform.position, waypointArray[currentWaypointIndex].position, bossSpeed * Time.deltaTime);
        Vector3 targetDirection = waypointArray[currentWaypointIndex].position - transform.position;
        //Vector3 newLookDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotateSpeed, 0.0f);
        Debug.DrawRay(transform.position, targetDirection, Color.cyan);
        //transform.rotation = Quaternion.LookRotation(newLookDirection);
        transform.LookAt(waypointArray[currentWaypointIndex], transform.up);
        Debug.DrawRay(transform.position, transform.position.normalized * 100f, Color.red);


        if (Vector3.Distance(transform.position, waypointArray[currentWaypointIndex].position) <= 2.0f)
        {
            currentWaypointIndex += 1;

            if (currentWaypointIndex == waypointArray.Length)
            {
                currentWaypointIndex = 0;
            }
        }
    }
}
