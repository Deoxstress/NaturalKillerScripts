using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{   

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    #region Singleton
    public static ObjectPool Instance;
    void Awake()
    {
        Instance = this;
    }
    #endregion

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    
    //Tuto Pooling https://learn.unity.com/tutorial/introduction-to-object-pooling#5ff8d015edbc2a002063971d
    //Tuto Brackeys ObjectPooling https://www.youtube.com/watch?v=tdSmKaJvCoA

    
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for(int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        if(objectToSpawn.GetComponent<Rigidbody>() != null)
            objectToSpawn.GetComponent<Rigidbody>().velocity = Vector3.zero;

        IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();
        if(pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    public bool IsQueueEmpty(string tag)
    {
        bool isEmpty = false;

        if(poolDictionary.ContainsKey(tag))
        {
            if(poolDictionary[tag].Count == 0)
            {
                isEmpty = true;
            }
        }

        return isEmpty;
    }

    public void GetAllPooledObjFromTagAndDeactivateBehaviour(string tag)
    {
        foreach(GameObject pooledObj in poolDictionary[tag])
        {
            pooledObj.GetComponent<ProjectileManager>().timerUntilDestroyed = 0.0f;
        }
    }
}
