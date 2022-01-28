using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using FIMSpace.FTail;
using Cinemachine;

public class BeholderController : MonoBehaviour
{
    public static BeholderController Instance { get; private set; }
    [SerializeField] Enemy boss;
    [SerializeField] bool phase2, phase3, rotateClockwise;
    [SerializeField] private int damageTresholdPhase2, damageTresholdPhase3, shieldHP, maxBossHp; // shield value is 4 - 10
    [SerializeField] GameObject shield, CristalDecalPrefab, homingMissilePrefab;
    [SerializeField] List<SpawnerFollowPlayer> spawnerFollowPlayerList = new List<SpawnerFollowPlayer>();
    [SerializeField] GameEvent OnShieldBreak, OnPhase2, OnPhase3, OnFinishIntro;
    [SerializeField] Transform spawnPointMissile;
    [SerializeField] Transform player, spawnPointForFallingCristals, playerGOFollowTransform, spawnPointStarHolder, spawnPointUltraRapidHolder;
    [SerializeField] Transform[] spawnPointsStarPattern, spawnPointsUltraRapidPattern;
    [SerializeField] private float timerUntilNewFallingCristal, timerUntilMissile, projSpeed, timerUntilIntro, elapsedTime, durationTotal, rotationSpeed, timerToSwitchRotation, initTimerToSwitchRotation;
    [SerializeField] private ObjectPool objPooler;
    [SerializeField] private SkinnedMeshRenderer[] skinnedMeshRenderers;
    public AudioSource audioSource;
    public AudioClip bossCrySound;
    public bool outroStart = true;
    public bool outroBossRunning = false;
    public bool outroGameEventInvoke = true;
    private bool cryBool;
    [SerializeField] private TailAnimator2[] tentaclesAnimator;
    [SerializeField] private Animator bodyDeathAnimator, cristalHolderAnimator;
    public CinemachineVirtualCamera cinemachineOutro;
    public CinemachineCamSwitch cmCamSwitch;
    private float initTimerCristal, initMissileTimer;
    public bool introCamSetup;
    public GameEvent OnOutroStart;
    [SerializeField] private CristalShardBehaviour[] crystalsArray;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        cryBool = false;
        audioSource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        boss = gameObject.GetComponent<Enemy>();
        player = FindObjectOfType<PlayerMovement>().transform;
        playerGOFollowTransform = GameObject.FindGameObjectWithTag("FollowGO").transform;
        spawnPointForFallingCristals = GameObject.FindGameObjectWithTag("FallingCristalSpawn").transform;
        initTimerCristal = timerUntilNewFallingCristal;
        initMissileTimer = timerUntilMissile;
        maxBossHp = boss.enemyHP;
        objPooler = ObjectPool.Instance;
        spawnPointStarHolder.parent = null;
        spawnPointUltraRapidHolder.parent = null;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (introCamSetup)
        {
            elapsedTime += Time.deltaTime;
            timerUntilIntro -= Time.deltaTime;
            transform.LookAt(playerGOFollowTransform, player.up);
            if (timerUntilIntro <= 0)
            {
                introCamSetup = false;
                OnFinishIntro.Invoke();
            }
        }
        else
        {
            if (!outroBossRunning)
            {
                if (shieldHP <= 0 && shield.activeInHierarchy)
                {
                    shield.SetActive(false);
                    boss.hasShield = false;
                    OnShieldBreak.Invoke();
                }

                timerUntilNewFallingCristal -= Time.deltaTime;
                timerUntilMissile -= Time.deltaTime;
                if (timerUntilNewFallingCristal <= 0 && !shield.activeInHierarchy)
                {
                    FallingCristalsPattern();
                    timerUntilNewFallingCristal = initTimerCristal;
                }
                transform.LookAt(playerGOFollowTransform, player.up);

                if (!phase2 && !phase3 && timerUntilMissile <= 0 && !shield.activeInHierarchy)
                {
                    //FireHomingMissiles();
                    StarPattern();
                    timerUntilMissile = initMissileTimer;
                }

                if ((phase2 || phase3) && timerUntilMissile <= 0 && !shield.activeInHierarchy)
                {
                    UltraRapidPattern();
                    timerUntilMissile = initMissileTimer;
                }

                if (boss.enemyHP <= maxBossHp * damageTresholdPhase2 / 100 && !phase2 && !phase3)
                {
                    OnPhase2.Invoke();
                    phase2 = true;
                }

                if (boss.enemyHP <= maxBossHp * damageTresholdPhase3 / 100 && phase2 && !phase3)
                {
                    OnPhase3.Invoke();
                    phase2 = false;
                    phase3 = true;
                }
                timerToSwitchRotation -= Time.deltaTime;
                if (timerToSwitchRotation < 0)
                {
                    rotateClockwise = !rotateClockwise;
                    timerToSwitchRotation = initTimerToSwitchRotation + Random.Range(-3, 4);
                }
            }

        }

        if (boss.enemyHP <= 0 && cryBool == false)
        {
            BossCry();
        }
    }

    void FixedUpdate()
    {
        if (rotateClockwise)
            spawnPointUltraRapidHolder.transform.Rotate(Vector3.up, rotationSpeed * Time.fixedDeltaTime);
        else
            spawnPointUltraRapidHolder.transform.Rotate(Vector3.up, -rotationSpeed * Time.fixedDeltaTime);
        
    }

    public void SetShieldHP(int newShieldValue)
    {
        shieldHP = newShieldValue;
        shield.SetActive(true);
        boss.hasShield = true;
    }

    public void SubstractShieldHp(int amountToSubstract)
    {
        shieldHP -= amountToSubstract;
    }

    public void AddSpawnerToList(SpawnerFollowPlayer newSpawnerToAdd)
    {
        spawnerFollowPlayerList.Add(newSpawnerToAdd);
    }

    public void SetAllSpawnerActive()
    {
        foreach (SpawnerFollowPlayer spawners in spawnerFollowPlayerList)
        {
            spawners.gameObject.SetActive(true);
            spawners.okToSpawn = true;
        }
    }

    public void SetAllSpawnerInactive()
    {
        foreach (SpawnerFollowPlayer spawners in spawnerFollowPlayerList)
        {
            spawners.gameObject.SetActive(false);
        }
    }

    public void FallingCristalsPattern()
    {
        GameObject CristalClone = objPooler.SpawnFromPool("CristalShard", spawnPointForFallingCristals.position, transform.localRotation);
        GameObject CristalDecalClone = Instantiate(CristalDecalPrefab, player.transform.position, player.rotation);
        CristalClone.GetComponent<CristalShardBehaviour>().playerPos = player.transform.position;
        CristalClone.GetComponent<CristalShardBehaviour>().isGrounded = false;
        CristalClone.GetComponent<CristalShardBehaviour>().speed = Random.Range(25f, 40f);
        CristalClone.GetComponent<CristalShardBehaviour>().rb.constraints = RigidbodyConstraints.None;
        CristalDecalClone.transform.LookAt(player, player.up);
    }

    public void FireHomingMissiles()
    {
        GameObject homingMissileClone;
        homingMissileClone = Instantiate(homingMissilePrefab, spawnPointMissile.position, Quaternion.identity);
        homingMissileClone.GetComponent<HomingBossMissile>().missileTarget = player;

    }
    public void SetInitTimerFallingCristals(float timer)
    {
        initTimerCristal = timer;
    }

    public void SetInitTimerHomingMissile(float timer)
    {
        initMissileTimer = timer;
    }

    public void SetRotationSpeed(float value)
    {
        rotationSpeed = value;
    }

    private void StarPattern()
    {
        foreach (Transform spawnPoint in spawnPointsStarPattern)
        {
            GameObject projectileClone = objPooler.SpawnFromPool("KryngstalProjs", spawnPoint.position, spawnPoint.rotation);
            ProjectileManager projManager = projectileClone.GetComponent<ProjectileManager>();
            projManager.timerUntilDestroyed = 3.5f;
            projManager.isSpawnedBySpawner = true;
            projManager.randomDirection = Vector3.forward;
            projManager.randomProjSpeed = projSpeed;
        }
    }

    private void BossCry()
    {
        audioSource.PlayOneShot(bossCrySound);
        cryBool = true;
    }

    private void UltraRapidPattern()
    {
        foreach(Transform spawnPoint in spawnPointsUltraRapidPattern)
        {
            GameObject projectileClone = objPooler.SpawnFromPool("KryngstalProjs", spawnPoint.position, spawnPoint.rotation);
            ProjectileManager projManager = projectileClone.GetComponent<ProjectileManager>();
            projManager.timerUntilDestroyed = 3.5f;
            projManager.isSpawnedBySpawner = true;
            projManager.randomDirection = Vector3.forward;
            projManager.randomProjSpeed = projSpeed;
        }
    }

    public void OutroBeholder(float elapsedTime)
    {
        if(outroStart)
        {
            crystalsArray = FindObjectsOfType<CristalShardBehaviour>();
            foreach (TailAnimator2 tentacle in tentaclesAnimator)
            {
                tentacle.enabled = false;
            }
            outroStart = false;
            foreach (CristalShardBehaviour crystals in crystalsArray)
            {
                crystals.gameObject.SetActive(false);
            }
            objPooler.GetAllPooledObjFromTagAndDeactivateBehaviour("KryngstalProjs");
            //bodyDeathAnimator.enabled = true;
            //cristalHolderAnimator.enabled = true;
        }     
        foreach (SkinnedMeshRenderer smr in skinnedMeshRenderers)
        {
            smr.materials[1].SetFloat("_Contrast", Mathf.Lerp(smr.materials[1].GetFloat("_Contrast"), 2000f, elapsedTime / durationTotal));
        }
    }
}
