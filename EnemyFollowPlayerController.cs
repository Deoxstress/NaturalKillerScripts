using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowPlayerController : MonoBehaviour
{
    [SerializeField] public float lifetime;
    [SerializeField] private float speed;
    [SerializeField, HideInInspector] private float initSpeed;
    [Range(5, 20)]
    [SerializeField] private int distanceThresholdToDash;
    [SerializeField] private float oppressingSpeed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform player, anchorPoint, pivotAnchorCurve;
    [SerializeField] private Transform[] anchorCurves;
    [SerializeField] private Enemy thisEnemy;
    [SerializeField] private bool isBossVariant;
    private LineRenderer laserBeamToBoss;
    private Transform attachedBoss;
    
    private Rigidbody playerRb;
    // Start is called before the first frame update

    void Awake()
    {
        initSpeed = speed;       
    }

    void Start()
    {
        if(isBossVariant)
        {
            laserBeamToBoss = GetComponentInChildren<LineRenderer>();
            attachedBoss = FindObjectOfType<BeholderController>().transform;
        }
        rb = gameObject.GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();       
        thisEnemy = gameObject.GetComponent<Enemy>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
        //Vector3 direction = player.position - rb.position;

        transform.LookAt(player, player.up);
        if(isBossVariant)
            pivotAnchorCurve.LookAt(attachedBoss, transform.up);
        MoveTowardsPlayer();
        /*
        if(thisEnemy.roundedDistanceToPlayer < distanceThresholdToDash)
        {
            speed = oppressingSpeed;
        }
        else
        {
            speed = initSpeed;
        }

        if (Vector3.SqrMagnitude(direction) >= 1.0f)
        {
            rb.velocity = direction * speed;
        }


        else
        {            
            rb.velocity = direction.normalized * speed;
        }
        */
    }

    private void LateUpdate()
    {
        if (isBossVariant)
            DrawLaser();
    }

    void Update()
    {
        lifetime -= Time.deltaTime;

        if(lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    void MoveTowardsPlayer()
    {
        Vector3 movement = Vector3.forward.normalized * speed;        
        rb.MovePosition(rb.position + transform.TransformDirection(movement) * Time.fixedDeltaTime);
    }

    //KnockBack On Collision :
    private void OnCollisionEnter(Collision collision)
    {   
        /*
        if(collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerMovement>().invulframes <= 0)
        {
            
            Destroy(this.gameObject);
        }
        */
    }

    void DrawLaser()
    {
        laserBeamToBoss.SetPosition(0, transform.position);
        RaycastHit hit;
        Debug.DrawLine(transform.position, attachedBoss.position, Color.green);
        //laserBeamToBoss.SetPosition(1, anchorPoint.position);
        laserBeamToBoss.SetPosition(1, anchorCurves[0].position);
        laserBeamToBoss.SetPosition(2, anchorCurves[1].position);
        laserBeamToBoss.SetPosition(3, anchorCurves[2].position);
        laserBeamToBoss.SetPosition(4, attachedBoss.position);
    }
}
