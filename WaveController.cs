using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WaveController : MonoBehaviour
{
    [SerializeField] private SpawnerFollowPlayer[] waveSpawn;
    [SerializeField] public List<Enemy> enemySpawned;
    [SerializeField] public Text waveCount;
    [SerializeField] private bool shieldDeactivated, initFight;
    [SerializeField] private GameObject beholderShield;
    [SerializeField] private float timerToReactivateShield, initTimer, timerToStartSpawn;
    [SerializeField] private SphereCollider beholderCollision;
    private Transform player;
    private Enemy attachedBoss;

    static WaveController instance;
    public static WaveController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<WaveController>();
            }
            return instance;
        }
    }
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().GetComponent<Transform>();
        attachedBoss = transform.parent.gameObject.GetComponent<Enemy>();
        waveCount = GameObject.Find("WaveText").GetComponent<Text>();
        waveCount.text = "Eliminate " + enemySpawned.Count + " more enemies to drop Beholder's shield";
        transform.parent = null;
        attachedBoss.GetComponent<GravityBody>().enabled = false;
        beholderCollision.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        attachedBoss.transform.LookAt(player, player.up);
        if (shieldDeactivated)
        {
            timerToReactivateShield -= Time.deltaTime;
            if (timerToReactivateShield <= 0)
            {
                foreach (SpawnerFollowPlayer spawner in waveSpawn)
                {
                    spawner.okToSpawn = true;
                }
                waveCount.text = "Eliminate " + enemySpawned.Count + " more enemies to drop Beholder's shield";
                beholderShield.SetActive(true);
                beholderCollision.enabled = false;
                shieldDeactivated = false;
                attachedBoss.hasShield = true;
            }
        }
        
        if(initFight)
        {
            timerToStartSpawn -= Time.deltaTime;
        }

        if (initFight && timerToStartSpawn <= 0)
        {
            foreach (SpawnerFollowPlayer spawner in waveSpawn)
            {
                spawner.okToSpawn = true;
            }
            waveCount.text = "Eliminate " + enemySpawned.Count + " more enemies to drop Beholder's shield";
            initFight = false;
        }

        if (attachedBoss.enemyHP <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnWaveEnemyDestroyed(Enemy enemy)
    {
        if (enemySpawned.Contains(enemy))
        {
            enemySpawned.Remove(enemy);
            waveCount.text = "Eliminate " + enemySpawned.Count + " more enemies to drop Beholder's shield";
            if (enemySpawned.Count == 0)
            {
                shieldDeactivated = true;
                timerToReactivateShield = initTimer;
                beholderShield.SetActive(false);
                beholderCollision.enabled = true;
                attachedBoss.hasShield = false;
                waveCount.text = "Damage it !";
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
