using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ForcefieldDemo;

[RequireComponent(typeof(Rigidbody))]
public class HomingProjectileController : MonoBehaviour
{
    public Transform target;
    public float speed;
    private Rigidbody rb;
    public float lifetime;
    private Vector3 missileMoveAmount;
    private Vector3 smoothMoveMissileVelocity;
    [SerializeField] private float frequencyMin;
    [SerializeField] private float frequencyMax;
    [SerializeField] private float magnitudeMin;
    [SerializeField] private float magnitudeMax;
    private Vector3 pos;
    private float elapsedTime;
    private Vector3 offset;
    private Vector3 direction;
    [SerializeField] private Animator missileAnim;
    private AudioSource aS;
    [SerializeField] private AudioClip shieldHit;

    private void Awake()
    {
        pos = transform.position;
        offset = transform.localPosition * Mathf.Sin(Random.Range(frequencyMin, frequencyMax)) * Random.Range(magnitudeMin, magnitudeMax);
    }
    void Start()
    {
        speed = Random.Range(speed, 3.0f);
        missileAnim = GetComponentInChildren<Animator>();
        missileAnim.SetInteger("RandomInt", Random.Range(0, 4));
        missileAnim.SetFloat("AnimationSpeedMultiplier", Random.Range(1.0f, 3.5f));
        rb = GetComponent<Rigidbody>();
        aS = Camera.main.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target == null)
        {
            return;
        }



        if (lifetime > 6.75f)
        {
            direction = target.position - rb.position + offset;
        }

        else if (lifetime <= 6.75f)
        {
            direction = target.position - rb.position;
            speed += 2;
        }

        transform.LookAt(target);

        if (Vector3.SqrMagnitude(direction) > 5.0f)
        {
            rb.velocity = direction * speed;
        }


        else
        {

            rb.velocity = direction.normalized * speed;
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetType() == typeof(Enemy))
        {
            StartDestroyAfterDelay();
            //Destroy(gameObject);
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "EnemyShield")
        {
            //other.GetComponent<ForcefieldImpact>().ApplyImpact(transform.position, -transform.forward);
            if (other.gameObject.GetComponent<Animator>() != null)
            {
                other.gameObject.GetComponent<Animator>().enabled = true;
                other.gameObject.GetComponent<Animator>().Play("BossCrystal_Shield");
                aS.PlayOneShot(shieldHit);
            }
            StartDestroyAfterDelay();
            //Destroy(gameObject);
        }
    }

    void Update()
    {

        lifetime -= Time.deltaTime;

        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }

        if (target == null)
        {
            StartDestroyAfterDelay();
            //Destroy(gameObject);
        }
    }

    public void StartDestroyAfterDelay(float delay = 1.0f)
    {
        StartCoroutine(DestroyAfterDelay(delay));
    }
    private IEnumerator DestroyAfterDelay(float delay)
    {
        GetComponent<Rigidbody>().isKinematic = true;
        missileAnim.enabled = false;
        foreach (Collider collider in GetComponents<SphereCollider>())
        {
            collider.enabled = false;
        }

        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

}
