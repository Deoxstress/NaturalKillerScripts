using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMidRange : MonoBehaviour
{
    [Header("Enemy Projectiles Spawn")]
    [SerializeField] private GameObject enemyProjPrefab;
    [Range(0.1f, 5.0f)]
    [SerializeField] private float timerToShoot;
    [Range(15.0f, 50.0f)]
    [SerializeField] private float projectileSpeed;
    private float initialTimer;
    public Transform SpawnPoint;
    [SerializeField] private Rigidbody rb;
    private Transform player;
    [SerializeField] private bool playerDetected;
    [SerializeField] private float speed;
    [SerializeField] private float maxDistanceToShoot;

    void Start()
    {
        playerDetected = false;
        initialTimer = timerToShoot;
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(player.position, transform.position) < maxDistanceToShoot)
        {
            //transform.LookAt(player, player.up);
            if (timerToShoot > 0)
            {
                timerToShoot -= Time.deltaTime;
            }

            else if (timerToShoot <= 0)
            {
                ShootTowardsPlayer();
                timerToShoot = initialTimer;
            }
        }
    }

    private void FixedUpdate()
    {
        if (playerDetected)
        {
            RunFromPlayer();
        }
    }

    void ShootTowardsPlayer()
    {
        //GameObject projClone = Instantiate(enemyProjPrefab, SpawnPoint.position, SpawnPoint.rotation);
        GameObject projClone = ObjectPool.Instance.SpawnFromPool("EnemyProj", SpawnPoint.position, SpawnPoint.rotation);
        ProjectileManager projManager = projClone.GetComponent<ProjectileManager>();
        projManager.timerUntilDestroyed = 2.0f;
        projManager.isSpawnedBySpawner = true;
        projManager.randomDirection = Vector3.forward;
        projManager.randomProjSpeed = projectileSpeed;
        Physics.IgnoreCollision(projClone.GetComponent<SphereCollider>(), gameObject.GetComponent<BoxCollider>());
    }

    void RunFromPlayer()
    {
        Vector3 movement = Vector3.back.normalized * speed;
        rb.MovePosition(rb.position + transform.TransformDirection(movement) * Time.fixedDeltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerDetected = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerDetected = false;
        }
    }
}
