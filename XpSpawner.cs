using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpSpawner : MonoBehaviour
{
    private ObjectPool objPool;
    public string nameOfPool;
    private GameObject poolCheck;
    public bool isReduced; // Check if number of xp dropped is below 50
    [SerializeField] private float timerToSpawnXp, initTimer;

    private void Start()
    {
        initTimer = timerToSpawnXp;
        objPool = ObjectPool.Instance;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isReduced)
        {
            if (!objPool.IsQueueEmpty(nameOfPool))
            {
                objPool.SpawnFromPool(nameOfPool, transform.position, transform.rotation);

            }
            else
            {
                Destroy(this.gameObject);
            }
        }

    }

    void Update()
    {
        if (isReduced)
        {
            timerToSpawnXp -= Time.deltaTime;
            if (timerToSpawnXp <= 0)
            {
                if (!objPool.IsQueueEmpty(nameOfPool))
                {
                    objPool.SpawnFromPool(nameOfPool, transform.position, transform.rotation);
                    timerToSpawnXp = initTimer;
                }
                else
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
