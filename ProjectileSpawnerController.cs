using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawnerController : MonoBehaviour
{
    [Header("Enemy Projectiles Spawn")]
    [SerializeField] private GameObject enemyProjPrefab;
    [SerializeField] private int enemyCount = 4;
    [SerializeField] private float fireSpeed;
    [Range(1.0f, 5.0f)]
    [SerializeField] private float timerToSpawn;
    private float initialTimer;
    public Transform[] SpawnPoints;
    void Start()
    {
        initialTimer = fireSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
        SpawnEnemyProjectiles();
    }

    void FixedUpdate()
    {
        transform.Rotate(Vector3.up, 30.0f * Time.fixedDeltaTime);
    }
    void SpawnEnemyProjectiles()
    {
        switch(enemyCount)
        {
            case 4:
                GameObject projClone4 = Instantiate(enemyProjPrefab, SpawnPoints[0].position, SpawnPoints[0].rotation);
                float localPointToWorld = SpawnPoints[0].position.x;
                projClone4.GetComponent<ProjectileManager>().isSpawnedBySpawner = true;
                projClone4.GetComponent<ProjectileManager>().randomDirection = Vector3.forward;
                projClone4.GetComponent<ProjectileManager>().randomProjSpeed = 15;
                Physics.IgnoreCollision(projClone4.GetComponent<SphereCollider>(), gameObject.GetComponent<BoxCollider>());
                enemyCount--;
                break;
            case 3:
                GameObject projClone3 = Instantiate(enemyProjPrefab, SpawnPoints[1].position, SpawnPoints[1].rotation);
                projClone3.GetComponent<ProjectileManager>().isSpawnedBySpawner = true;
                projClone3.GetComponent<ProjectileManager>().randomDirection = Vector3.forward;
                projClone3.GetComponent<ProjectileManager>().randomProjSpeed = 15;
                Physics.IgnoreCollision(projClone3.GetComponent<SphereCollider>(), gameObject.GetComponent<BoxCollider>());
                enemyCount--; 
                break;
            case 2:
                GameObject projClone2 = Instantiate(enemyProjPrefab, SpawnPoints[2].position, SpawnPoints[2].rotation);
                projClone2.GetComponent<ProjectileManager>().isSpawnedBySpawner = true;
                projClone2.GetComponent<ProjectileManager>().randomDirection = Vector3.forward;
                projClone2.GetComponent<ProjectileManager>().randomProjSpeed = 15;
                Physics.IgnoreCollision(projClone2.GetComponent<SphereCollider>(), gameObject.GetComponent<BoxCollider>());
                enemyCount--; 
                break;
            case 1:
                GameObject projClone1 = Instantiate(enemyProjPrefab, SpawnPoints[3].position, SpawnPoints[3].rotation);
                projClone1.GetComponent<ProjectileManager>().isSpawnedBySpawner = true;
                projClone1.GetComponent<ProjectileManager>().randomDirection = Vector3.forward;
                projClone1.GetComponent<ProjectileManager>().randomProjSpeed = 15;
                Physics.IgnoreCollision(projClone1.GetComponent<SphereCollider>(), gameObject.GetComponent<BoxCollider>());
                enemyCount--; 
                break;
        }

        if (enemyCount == 0 && timerToSpawn > 0)
        {
            timerToSpawn -= Time.deltaTime;
        }

        else if (timerToSpawn <= 0)
        {
            enemyCount = 4;
            timerToSpawn = initialTimer;
        }
    }

}
