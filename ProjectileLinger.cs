using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLinger : MonoBehaviour
{

    [SerializeField] public float lifetime;
    public bool enableLingeringEffect;
    [SerializeField] private bool isFireCol, colliderEnabled;
    private SphereCollider fireColumnCollider;
    private Transform player;

    private void Awake()
    {
        if (isFireCol)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            fireColumnCollider = GetComponentInParent<SphereCollider>();
        }
            
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enableLingeringEffect)
        {           
            lifetime -= Time.deltaTime;
        }

        if(lifetime <= 1 && isFireCol && !colliderEnabled)
        {           
            fireColumnCollider.enabled = true;
            colliderEnabled = true;
        }

        if(lifetime <= 0)
        {          
            Destroy(this.gameObject);           
        }

        if(isFireCol)
            transform.LookAt(player, player.up);
    }
}
