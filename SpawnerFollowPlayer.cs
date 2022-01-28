using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerFollowPlayer : MonoBehaviour
{

    [SerializeField] public GameObject enemyFollowPrefab;
    [SerializeField] private float timerToSpawn;
    [SerializeField] private float initTimerToSpawn;
    [SerializeField] private GameObject enemyCloned, SpawnVFXPrefab;
    [SerializeField] public bool okToSpawn, isFromWave, isNotFromWave, vfxSpawned, isFromBoss, isFromKryngstalBoss;
    [SerializeField] private WaveController assignedWave;
    public WaveManager assignedWaveMan;
    [SerializeField] private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        if (isFromWave && isFromBoss)
        {            
            assignedWave = GetComponentInParent<WaveController>();           
        }     
        initTimerToSpawn = timerToSpawn;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (okToSpawn && isFromWave)
        {
            if (enemyCloned == null)
            {
                timerToSpawn -= Time.deltaTime;
                if(timerToSpawn <= 1f && !vfxSpawned)
                {
                    GameObject spawnVfxClone = Instantiate(SpawnVFXPrefab, transform.position, Quaternion.identity);
                    spawnVfxClone.GetComponent<ProjectileLinger>().enabled = true;
                    spawnVfxClone.GetComponent<ProjectileLinger>().enableLingeringEffect = true;
                    spawnVfxClone.transform.parent = null;
                    spawnVfxClone.transform.LookAt(player, player.up);
                    vfxSpawned = true;
                }

                else if (timerToSpawn <= 0 && isFromBoss)
                {
                    enemyCloned = SpawnEnemy();
                    okToSpawn = false;
                    assignedWave.enemySpawned.Add(enemyCloned.GetComponent<Enemy>());
                    //assignedWave.waveCount.text = "Eliminate " + assignedWave.enemySpawned.Count + " more enemies to drop Beholder's Shield";
                    timerToSpawn = initTimerToSpawn;
                    vfxSpawned = false;
                }
                else if (timerToSpawn <= 0 && !isFromBoss)
                {
                    enemyCloned = SpawnEnemy();
                    okToSpawn = false;
                    assignedWaveMan.enemySpawned.Add(enemyCloned.GetComponent<Enemy>());
                    //assignedWaveMan.waveCount.text = "Eliminate " + assignedWaveMan.enemySpawned.Count + " more enemies !";
                    timerToSpawn = initTimerToSpawn;
                    vfxSpawned = false;
                }
            }
        }
        if (isNotFromWave)
        {
            if (enemyCloned == null)
            {
                timerToSpawn -= Time.deltaTime;
                if (timerToSpawn <= 1f && !vfxSpawned)
                {
                    GameObject spawnVfxClone = Instantiate(SpawnVFXPrefab, transform.position, Quaternion.identity);
                    spawnVfxClone.GetComponent<ProjectileLinger>().enabled = true;
                    spawnVfxClone.GetComponent<ProjectileLinger>().enableLingeringEffect = true;
                    spawnVfxClone.transform.parent = null;
                    spawnVfxClone.transform.LookAt(player, player.up);
                    vfxSpawned = true;
                }

                else if (timerToSpawn <= 0)
                {
                    enemyCloned = SpawnEnemy();
                    timerToSpawn = initTimerToSpawn;
                    vfxSpawned = false;
                }
            }
        }
        if(okToSpawn && isFromKryngstalBoss)
        {
            Debug.Log("isEnemyNull");
            if (enemyCloned == null)
            {
                Debug.Log("EnemyNull");
                timerToSpawn -= Time.deltaTime;
                if (timerToSpawn <= 2f && !vfxSpawned)
                {
                    GameObject spawnVfxClone = Instantiate(SpawnVFXPrefab, transform.position, Quaternion.identity);
                    spawnVfxClone.GetComponent<ProjectileLinger>().enabled = true;
                    spawnVfxClone.GetComponent<ProjectileLinger>().enableLingeringEffect = true;
                    spawnVfxClone.transform.parent = null;
                    spawnVfxClone.transform.LookAt(player, player.up);
                    vfxSpawned = true;
                }

                else if (timerToSpawn <= 0 && isFromBoss)
                {
                    enemyCloned = SpawnEnemy();
                    okToSpawn = false;
                    timerToSpawn = initTimerToSpawn;
                    vfxSpawned = false;
                }
            }
        }
    }
    GameObject SpawnEnemy()
    {
        GameObject enemyClone = Instantiate(enemyFollowPrefab, transform.position, Quaternion.identity);
        return enemyClone;
    }
}
