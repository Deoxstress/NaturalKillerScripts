using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ForcefieldDemo;
using TMPro;

public class Enemy : MonoBehaviour
{
    [SerializeField] public float distanceToPlayer, bossOutroTimer;
    public int roundedDistanceToPlayer;
    [SerializeField] private Transform player;
    [SerializeField] public int enemyHP = 3;
    [SerializeField] private GameObject UpgradePrefab, explosionVfxPrefab, enemyHitVFXPrefab;
    [SerializeField] public bool isInList;
    [SerializeField] public GameObject target;
    [SerializeField] private GameObject targetPivot;
    [SerializeField] public int closeCounter;
    [SerializeField, HideInInspector] private int maxCloseCounter;
    [SerializeField] private GameObject counterGO;
    [SerializeField] private bool deactivateLock;
    [SerializeField] private PlayerValues PlayerLevelValues;
    [SerializeField] private Transform mainCamera;
    [SerializeField] public bool hasShield;
    [SerializeField] private GameObject Shield;
    [SerializeField] private GameObject lockUIMax;
    [SerializeField] private Animator targetAnimator;

    [SerializeField] private AudioSource audioSourceSFX;
    [SerializeField] private AudioSource audioSourceBackground;
    [SerializeField] private AudioClip exploMusic;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hitEnemy;
    [SerializeField] private AudioClip maxLockSound;
    [SerializeField] private bool isBoss;
    [SerializeField] private bool isBeholder, isMoonBoss;
    [SerializeField] private Animator hitAnimator;
    public float elapsedTimeBoss = 0f;
    [SerializeField] GameEvent OnWaveBeholderEnemyDied;

    [HideInInspector]
    public bool isMaxHit = false;
    public Animator animator;
    [SerializeField] private GameObject KryngstalHazard;



    void Awake()
    {
        closeCounter = 0;
        counterGO.SetActive(false);
        target.SetActive(false);
        lockUIMax.SetActive(false);
        deactivateLock = false;
        isInList = false;
        maxCloseCounter = PlayerLevelValues.maxLockOnTargets;
        audioSourceSFX = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
    }
    void Start()
    {
        animator.enabled = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        switch(PlayerLevelValues.lockTimerToAddEnemyToScan)
        {
            case 0.3f:
                targetAnimator.speed = 1.0f;
                animator.speed = 1.0f;
                break;
            case 0.2f:
                targetAnimator.speed = 1.5f;
                animator.speed = 1.5f;
                break;
            case 0.1f:
                targetAnimator.speed = 3.0f;
                animator.speed = 3.0f;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(player.position, transform.position);
        roundedDistanceToPlayer = (int)Mathf.Round(distanceToPlayer);
        targetPivot.transform.LookAt(player, player.up);

        
        if(enemyHP <= PlayerLevelValues.maxLockOnTargets)
        {
            maxCloseCounter = enemyHP;
        }

        if(enemyHP <= 0)
        {           
            if (isBoss)
            {
                
                bossOutroTimer -= Time.deltaTime;
                if (BossController.Instance != null && BossController.Instance.gameObject.activeInHierarchy) //BossTuto death animation
                {
                    elapsedTimeBoss += Time.deltaTime;
                    BossController.Instance.OutroLocalScale(elapsedTimeBoss);
                }
                if (BossController.Instance != null && BossController.Instance.outroStart && BossController.Instance.gameObject.activeInHierarchy)
                {
                    BossController.Instance.OnBossFinishOutro.Invoke();
                    BossController.Instance.outroStart = false;
                }
                if (bossOutroTimer <= 0 && BossController.Instance != null)
                {
                    BossController.Instance.cmCamSwitch.SwitchStateMenu("PlayerCamera");
                    CinemachineCameraShake.Instance.CameraShakeOnSpecificCamera(50f, 1.5f, BossController.Instance.cineMachineOutro);
                    GameObject cloneMsUpgrade = Instantiate(UpgradePrefab, transform.position, Quaternion.identity);
                    audioSourceBackground.clip = exploMusic;
                    audioSourceBackground.Play();
                    Destroy(gameObject);
                    KryngstalHazard.SetActive(false);
                }

                if(BeholderController.Instance != null && BeholderController.Instance.gameObject.activeInHierarchy)
                {
                    elapsedTimeBoss += Time.deltaTime;
                    BeholderController.Instance.OutroBeholder(elapsedTimeBoss);
                    BeholderController.Instance.outroBossRunning = true;
                }

                if (BeholderController.Instance != null && BeholderController.Instance.outroGameEventInvoke && BeholderController.Instance.gameObject.activeInHierarchy)
                {
                    BeholderController.Instance.OnOutroStart.Invoke();
                    BeholderController.Instance.outroGameEventInvoke = false;
                }

                if (bossOutroTimer <= 0 && BeholderController.Instance != null && BeholderController.Instance.gameObject.activeInHierarchy)
                {
                    BeholderController.Instance.cmCamSwitch.SwitchStateMenu("PlayerCamera");
                    CinemachineCameraShake.Instance.CameraShakeOnSpecificCamera(7.5f, 1.5f, BeholderController.Instance.cinemachineOutro);
                    GameObject cloneMsUpgrade = Instantiate(UpgradePrefab, transform.position, Quaternion.identity);
                    audioSourceBackground.clip = exploMusic;
                    audioSourceBackground.Play();
                    Destroy(gameObject);
                    KryngstalHazard.SetActive(false);
                }
                if (MoonBossController.Instance != null && MoonBossController.Instance.outroGameEventInvoke && MoonBossController.Instance.gameObject.activeInHierarchy)
                {
                    MoonBossController.Instance.OnOutroStart.Invoke();
                    MoonBossController.Instance.outroGameEventInvoke = false;
                }

                if (bossOutroTimer <= 0 && MoonBossController.Instance != null && MoonBossController.Instance.gameObject.activeInHierarchy)
                {
                    MoonBossController.Instance.cmSwitch.SwitchStateMenu("PlayerCamera");
                    CinemachineCameraShake.Instance.CameraShakeOnSpecificCamera(7.5f, 1.0f, MoonBossController.Instance.cinemachineOutro);
                    GameObject cloneMsUpgrade = Instantiate(UpgradePrefab, transform.position, Quaternion.identity);
                    audioSourceBackground.clip = exploMusic;
                    audioSourceBackground.Play();
                    Destroy(gameObject);
                }
            }
            else
            {
                GameObject cloneMsUpgrade = Instantiate(UpgradePrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            
        }

        if (closeCounter > 0 && closeCounter < maxCloseCounter)
        {
            deactivateLock = true;
            counterGO.SetActive(true);
            counterGO.GetComponent<TMP_Text>().text = "x" + closeCounter;
            animator.enabled = true;
            isMaxHit = false;
        }

        else if (closeCounter >= maxCloseCounter)
        {
            closeCounter = maxCloseCounter;
            //counterGO.GetComponent<TMP_Text>().text = "MAX";
            counterGO.GetComponent<TMP_Text>().text = "x" + closeCounter;
            animator.enabled = true;
            if (isMaxHit == false && enemyHP > 0)
            {
                audioSourceSFX.PlayOneShot(maxLockSound);
                isMaxHit = true;
            }
            animator.Play("Lock_counter_max");
            CinemachineCameraShake.Instance.CameraShake(2.0f, 0.1f);
            target.SetActive(false);
            lockUIMax.SetActive(true);
        }
        
        if (closeCounter <= 0 && deactivateLock)
        {
            counterGO.SetActive(false);
            target.SetActive(false);
            lockUIMax.SetActive(false);
            deactivateLock = false;
            animator.enabled = false;
            isMaxHit = false;
            WeaponSystem.Instance.OnEnemyDestroyed(this);           
        }

        if(roundedDistanceToPlayer >= 42 && deactivateLock)
        {
            WeaponSystem.Instance.OnEnemyDestroyed(this);
            counterGO.GetComponent<TMP_Text>().fontSize = 16;
            isMaxHit = false;
            target.SetActive(false);
            lockUIMax.SetActive(false);
            counterGO.SetActive(false);
            deactivateLock = false;
            animator.enabled = false;
            closeCounter = 0;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.tag == "PlayerMissile")
        {
            if (hasShield)
            {
                other.GetComponent<HomingProjectileController>().StartDestroyAfterDelay();
                RaycastHit hit;
                if (Physics.Raycast(other.transform.position, Shield.transform.position - other.transform.position, out hit))
                {
                    if(hit.collider.name == "Shield")
                    {
                        Shield.GetComponent<ForcefieldImpact>().ApplyImpact(hit.point, hit.normal);
                    }                                                       
                }
                
            }
            else
            {
                audioSourceSFX.PlayOneShot(hitEnemy);
                enemyHP -= 1;
                CinemachineCameraShake.Instance.CameraShake(2.65f, 0.2f);
                other.GetComponent<HomingProjectileController>().StartDestroyAfterDelay();
                GameObject enemyHitVfxClone = Instantiate(enemyHitVFXPrefab, /*(Random.insideUnitSphere * 5) + */other.transform.position, Quaternion.identity);
                enemyHitVfxClone.GetComponent<ProjectileLinger>().enabled = true;
                enemyHitVfxClone.GetComponent<ProjectileLinger>().enableLingeringEffect = true;
                enemyHitVfxClone.transform.LookAt(mainCamera, mainCamera.up);
                if (isMoonBoss)
                {
                    hitAnimator.enabled = true;
                    hitAnimator.Play("BossMercune_Hit");
                }
                if (isBeholder)
                {
                    hitAnimator.enabled = true;
                    hitAnimator.Play("Hit_Animation");
                }             
            }
            
            //Destroy(other.gameObject);
        }
        
        if(other.gameObject.tag == "Bomb")
        {
            enemyHP -= 20;
        }
    }


    private void OnDestroy()
    {
        if(WeaponSystem.Instance != null)      
            WeaponSystem.Instance.OnEnemyDestroyed(this);  
        if(ObjectiveController.Instance != null)
            ObjectiveController.Instance.OnObjectiveEnemyDestroyed(this);

        if(WaveController.Instance != null)
            WaveController.Instance.OnWaveEnemyDestroyed(this);

        else if (WaveManager.Instance != null)
            WaveManager.Instance.OnWaveManEnemyDestroyed(this);

        OnWaveBeholderEnemyDied?.Invoke(); // "?" means if not null, do something.
        GameObject explosionVfxClone = Instantiate(explosionVfxPrefab, transform.position, Quaternion.identity);
        if(isBoss)
            explosionVfxClone.transform.localScale = new Vector3(25, 25, 25);       
        explosionVfxClone.GetComponent<ProjectileLinger>().enabled = true;
        explosionVfxClone.GetComponent<ProjectileLinger>().enableLingeringEffect = true;
        explosionVfxClone.transform.LookAt(player, player.up);
        audioSourceSFX.PlayOneShot(deathSound);
    }    
}
