using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public Vector3 randomDirection;
    private Rigidbody projRB;
    private Vector3 spawnPoint;
    public float randomProjSpeed;
    private Vector3 moveAmount;
    private Vector3 smoothMoveVelocity;
    public bool isSpawnedBySpawner;
    public float timerUntilDestroyed = 2.0f;
    private Animator projectileDissolveAnim;


    void Start()
    {
        projectileDissolveAnim = gameObject.GetComponent<Animator>();
        projRB = GetComponent<Rigidbody>();

        if (isSpawnedBySpawner)
        {

        }
        else
        {
            randomDirection = new Vector3(Random.Range(-1f, 1f), 0.0f, Random.Range(-1f, 1f)).normalized;
            //randomProjSpeed = Random.Range(8, 15);
            randomProjSpeed = 15.0f;
        }

    }

    // Update is called once per frame
    void Update()
    {
        timerUntilDestroyed -= Time.deltaTime;

        if (timerUntilDestroyed <= 0)
        {
            projectileDissolveAnim.enabled = true;
            projectileDissolveAnim.Play("Dissolve_Projectile");
            //Destroy(gameObject);
        }
        moveAmount = Vector3.SmoothDamp(moveAmount, randomDirection * randomProjSpeed, ref smoothMoveVelocity, 0.15f);
    }

    void FixedUpdate()
    {
        projRB.MovePosition(projRB.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bomb")
        {
            Destroy(this.gameObject);
        }

        if (other.gameObject.tag == "Rock")
        {
            projectileDissolveAnim.enabled = true;
            projectileDissolveAnim.Play("Dissolve_Projectile");
            //Destroy(this.gameObject);
        }

        if (other.gameObject.tag == "CristalShard")
        {
            projectileDissolveAnim.enabled = true;
            projectileDissolveAnim.Play("Dissolve_Projectile");
            //Destroy(this.gameObject);
        }
    }

    /*void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Rock")
        {
            projectileDissolveAnim.enabled = true;
            projectileDissolveAnim.Play("Dissolve_Projectile");
            //Destroy(this.gameObject);
        }
    }*/

    public void SetObjectInactive()
    {
        projectileDissolveAnim.enabled = false;
        gameObject.SetActive(false);
    }

}
