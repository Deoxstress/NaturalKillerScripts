using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private Enemy[] enemiesWithin;

    [SerializeField] private float bombLifetime;

    private void OnEnable()
    {
        bombLifetime = 0.5f;
       // enemiesWithin = GameObject.FindObjectsOfType<Enemy>();
    }

    private void OnDisable()
    {
        /*
        for (int i = 0; i < enemiesWithin.Length; i++)
        {
            enemiesWithin[i] = null;
        }
        */
    }
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();    
    }

    // Update is called once per frame
    void Update()
    {
              
        bombLifetime -= Time.deltaTime;
       
        if(bombLifetime <= 0)
        {            
            DefuseBomb();
        }
    }

    public void DefuseBomb()
    {
        this.gameObject.SetActive(false);
    }
}
