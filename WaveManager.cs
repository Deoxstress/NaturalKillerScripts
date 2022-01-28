using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private List<SpawnerFollowPlayer> waveSpawn;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject spawnerPrefab;
    [SerializeField] public List<Enemy> enemySpawned;
    [SerializeField] public Text waveCount;
    [SerializeField] private bool previousWaveDone, initFight;
    [SerializeField] private float timerToSpawnNewWave, initTimer, timerToStartSpawn;
    [Range(0, 6)]
    [SerializeField] private int[] waveEnemiesNumber;
    private int ActualWave;
    private Transform player;

    static WaveManager instance;
    public static WaveManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<WaveManager>();
            }
            return instance;
        }
    }
    void Start()
    {
        ActualWave = waveEnemiesNumber.Length;
        player = FindObjectOfType<PlayerMovement>().GetComponent<Transform>();
        waveCount = GameObject.Find("WaveText").GetComponent<Text>();
        //waveCount.text = "Eliminate " + enemySpawned.Count + " more enemies !";
        transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (previousWaveDone)
        {
            timerToSpawnNewWave -= Time.deltaTime;
            if (waveSpawn.Count == 0)
            {
                if (timerToSpawnNewWave <= 0 && ActualWave != 0)
                {
                    SpawnWave(waveEnemiesNumber[ActualWave - 1]);
                    Debug.Log(waveEnemiesNumber[ActualWave - 1]);
                    foreach (SpawnerFollowPlayer spawner in waveSpawn)
                    {
                        spawner.okToSpawn = true;
                    }
                    //waveCount.text = "Eliminate " + enemySpawned.Count + " more enemies !";
                    previousWaveDone = false;
                }
                else if (ActualWave == 0)
                {

                    Destroy(this.gameObject);

                }
            }
            else
            {
                foreach (SpawnerFollowPlayer spawner in waveSpawn)
                {
                    Destroy(spawner);                   
                }
                waveSpawn.Clear();
            }

        }

        if (initFight)
        {
            timerToStartSpawn -= Time.deltaTime;
        }

        if (initFight && timerToStartSpawn <= 0)
        {
            SpawnWave(waveEnemiesNumber[ActualWave - 1]);
            foreach (SpawnerFollowPlayer spawner in waveSpawn)
            {
                spawner.okToSpawn = true;
            }
            //waveCount.text = "Eliminate " + enemySpawned.Count + " more enemies !";
            initFight = false;
        }

    }


    void SpawnWave(int EnemiesInWave)
    {
        for (int i = EnemiesInWave; i >= 0; i--)
        {
            GameObject spawnerClone = Instantiate(spawnerPrefab, spawnPoints[i].position, Quaternion.identity);
            spawnerClone.GetComponent<SpawnerFollowPlayer>().isFromWave = true;
            spawnerClone.GetComponent<SpawnerFollowPlayer>().isNotFromWave = false;
            spawnerClone.GetComponent<SpawnerFollowPlayer>().okToSpawn = false;
            spawnerClone.GetComponent<SpawnerFollowPlayer>().isFromBoss = false;
            spawnerClone.GetComponent<SpawnerFollowPlayer>().assignedWaveMan = this.GetComponent<WaveManager>();
            waveSpawn.Add(spawnerClone.GetComponent<SpawnerFollowPlayer>());
        }
    }
    public void OnWaveManEnemyDestroyed(Enemy enemy)
    {
        if (enemySpawned.Contains(enemy))
        {
            enemySpawned.Remove(enemy);
            //waveCount.text = "Eliminate " + enemySpawned.Count + " more enemies !";
            if (enemySpawned.Count == 0)
            {

                timerToSpawnNewWave = initTimer;
                //waveCount.text = "Next Wave Incoming !";
                previousWaveDone = true;
                ActualWave--;

            }
        }
    }

    void OnDestroy()
    {
        foreach (SpawnerFollowPlayer spawner in waveSpawn)
        {
            Destroy(spawner);
        }
        waveCount.gameObject.SetActive(false);
    }
}
