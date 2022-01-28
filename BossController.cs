using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FIMSpace.FTail;
using Cinemachine;

public class BossController : MonoBehaviour
{
    public static BossController Instance { get; private set; }
    [SerializeField] private Enemy attachedBoss;
    [SerializeField] private int damageTresholdPhase2, damageTresholdPhase3, helixProjectileSpawnNumber, maxBossHp, bulletsIntantiated;
    [SerializeField] private bool phase1, phase2, phase3, triggerLifetimeVfx, introFinished, introCamSetup, rotateClockwise;
    [SerializeField] private GameObject projectile, spawnPointHolder, fireColumnPrefab, spawnPointHolderPizzaPattern;
    [SerializeField] private float projSpeed, projectileSpawnSpeed, initProjSpawnSpeed, delayToSpawnInitialBullets, rotationSpeed, columnSpawnSpeed, initColumnSpawnSpeed, timerBetweenPhase;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform[] spawnPointsForPizzaPattern;
    [SerializeField] private float timerToSwitchAngle, initTimerToSwitchAngle, elapsedTime, durationTotal, durationTotalOutro, timerToSwitchRotation, initTimerToSwitchRotation;
    [SerializeField] private GameObject prefabEnemySpawn, spawnVFXClone;
    [SerializeField] private GameEvent OnBossFinishIntro;
    public GameEvent OnBossFinishOutro;
    public bool outroStart = true;
    public CinemachineCamSwitch cmCamSwitch;
    float scaledIntensity = 1.0f;
    public CinemachineVirtualCamera cineMachineOutro;
    public AudioSource audioSource;
    public AudioClip bossCrySound;
    private bool cryBool;

    private Transform player;

    private void Awake()
    {
        Instance = this;
        cmCamSwitch = FindObjectOfType<CinemachineCamSwitch>();
    }
    // Start is called before the first frame update
    void Start()
    {
        cryBool = false;
        audioSource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        phase1 = false;
        phase2 = false;
        attachedBoss = gameObject.GetComponent<Enemy>();
        projectileSpawnSpeed = initProjSpawnSpeed;
        columnSpawnSpeed = initColumnSpawnSpeed;
        maxBossHp = attachedBoss.enemyHP;
        triggerLifetimeVfx = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        attachedBoss.transform.LookAt(player, player.up);
        if (!phase1 && !phase2 && !phase3)
        {
            if(introCamSetup)
            {
                if (elapsedTime < durationTotal)
                {
                    elapsedTime += Time.deltaTime;
                    IntroLocalScale(triggerLifetimeVfx);
                }
                else
                {
                    if (!introFinished)
                    {
                        OnBossFinishIntro.Invoke();
                        introFinished = true;
                    }                  
                    transform.LookAt(player, transform.up);
                    delayToSpawnInitialBullets -= Time.deltaTime;
                    if (delayToSpawnInitialBullets <= 0)
                    {
                        spawnPointHolder.transform.localEulerAngles = new Vector3(0, 0, 0);
                        spawnPointHolder.transform.parent = null;
                        spawnPoints[0].localEulerAngles = new Vector3(0, 0, 0);
                        spawnPoints[1].localEulerAngles = new Vector3(0, 120, 0);
                        spawnPoints[2].localEulerAngles = new Vector3(0, 240, 0);
                        spawnPointHolderPizzaPattern.transform.localEulerAngles = new Vector3(0, 0, 0);
                        spawnPointHolderPizzaPattern.transform.parent = null;
                        phase1 = true;                   
                    }
                }
            }
        }

        else if (phase1)
        {

            if (projectileSpawnSpeed > 0)
            {
                projectileSpawnSpeed -= Time.deltaTime;
            }
            else if (projectileSpawnSpeed <= 0)
            {
                //helixProjectileSpawnNumber = 3;                
                projectileSpawnSpeed = initProjSpawnSpeed;
                bulletsIntantiated += 3;
                HelixPattern();
            }
            if (columnSpawnSpeed > 0)
            {
                columnSpawnSpeed -= Time.deltaTime;
            }
            else if (columnSpawnSpeed <= 0)
            {
                //FireColumnPattern();
                columnSpawnSpeed = initColumnSpawnSpeed;
            }

        }

        if (attachedBoss.enemyHP > maxBossHp * damageTresholdPhase3 /100 && attachedBoss.enemyHP <= maxBossHp * damageTresholdPhase2 / 100 && !phase2 && !phase3)
        {
            phase1 = false;          
            timerBetweenPhase -= Time.deltaTime;           
        }
        if(timerBetweenPhase <= 0.75f && !phase2 && !phase3)
        {
            gameObject.GetComponentInChildren<TailAnimator2>().enabled = true;
            if (timerBetweenPhase <= 0 && !phase2)
            {
                phase2 = true;
                timerBetweenPhase = 0.75f;
            }
        }

        

        if (phase2)
        {
            if (projectileSpawnSpeed > 0)
            {
                projectileSpawnSpeed -= Time.deltaTime;
            }
            else if (projectileSpawnSpeed <= 0)
            {
                helixProjectileSpawnNumber = 3;
                projectileSpawnSpeed = initProjSpawnSpeed * 2;
                bulletsIntantiated += 13;
                PizzaPattern();
            }
        }

        if (attachedBoss.enemyHP <= maxBossHp * damageTresholdPhase3 / 100  && !phase3)
        {
            phase2 = false;
            timerBetweenPhase -= Time.deltaTime;
            
        }
        if (attachedBoss.enemyHP <= maxBossHp * damageTresholdPhase3 / 100 && timerBetweenPhase <= 0 && !phase3)
        {
            phase1 = false;
            phase2 = false;
            phase3 = true;
        }

        if (phase3)
        {
            timerToSwitchAngle -= Time.deltaTime;
        }
        if (phase3 && timerToSwitchAngle <= 0 && !cryBool)
        {
            spawnPointHolderPizzaPattern.transform.localEulerAngles = new Vector3(0, Random.Range(0, 360), 0);
            PizzaPattern();
            timerToSwitchAngle = initTimerToSwitchAngle;
        }
        timerToSwitchRotation -= Time.deltaTime;
        if(timerToSwitchRotation < 0)
        {
            rotateClockwise = !rotateClockwise;
            timerToSwitchRotation = initTimerToSwitchRotation + Random.Range(-3, 4);
        }
    }

    void FixedUpdate()
    {
        timerToSwitchAngle -= Time.fixedDeltaTime;
        if(rotateClockwise)
        {
            spawnPointHolder.transform.Rotate(Vector3.up, rotationSpeed * Time.fixedDeltaTime);
        }
        if (!rotateClockwise)
        {
            spawnPointHolder.transform.Rotate(Vector3.up, -rotationSpeed * Time.fixedDeltaTime);
        }
        if(phase1 || phase2)
        {
            if(rotateClockwise)
                spawnPointHolderPizzaPattern.transform.Rotate(Vector3.up, rotationSpeed * Time.fixedDeltaTime);
            else
                spawnPointHolderPizzaPattern.transform.Rotate(Vector3.up, -rotationSpeed * Time.fixedDeltaTime);
        }
    }
    void HelixPattern()
    {        
        foreach (Transform spawnPointHelix in spawnPoints)
        {
            GameObject projectileClone = ObjectPool.Instance.SpawnFromPool("BossProjectile", spawnPointHelix.position, spawnPointHelix.rotation);
            ProjectileManager projManager = projectileClone.GetComponent<ProjectileManager>();
            projManager.timerUntilDestroyed = 3.5f;
            projManager.isSpawnedBySpawner = true;
            projManager.randomDirection = Vector3.forward;
            projManager.randomProjSpeed = projSpeed;          
        }
    }

    /*void FireColumnPattern()
    {
        GameObject fireColClone = Instantiate(fireColumnPrefab, player.position, Quaternion.identity);
        fireColClone.GetComponent<ProjectileLinger>().enabled = true;
        fireColClone.GetComponent<ProjectileLinger>().enableLingeringEffect = true;
    }*/

    void PizzaPattern()
    {
        foreach (Transform spawnPoint in spawnPointsForPizzaPattern)
        {
            //GameObject projectileClone = Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);
            GameObject projectileClone = ObjectPool.Instance.SpawnFromPool("BossProjectile", spawnPoint.position, spawnPoint.rotation);
            ProjectileManager projManager = projectileClone.GetComponent<ProjectileManager>();
            projManager.isSpawnedBySpawner = true;
            projManager.randomDirection = Vector3.forward;
            if (phase3)
            {
                projManager.timerUntilDestroyed = 12.0f;
                projManager.randomProjSpeed = projSpeed / 3.0f;
            }
            else
            {
                projManager.timerUntilDestroyed = 3.5f;
                projManager.randomProjSpeed = projSpeed / 1.40f;
            }         
        }
    }
    
    void IntroLocalScale(bool triggerLifetime)
    {       
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(8, 8, 8), elapsedTime / durationTotal);       
        /*if(spawnVFXClone != null)
            spawnVFXClone.transform.localScale = Vector3.Lerp(new Vector3(14, 14, 14), new Vector3(20, 20, 20), elapsedTime / durationTotal);
        if (triggerLifetime)
        {
            spawnVFXClone = Instantiate(prefabEnemySpawn, transform.position, Quaternion.identity);            
            spawnVFXClone.GetComponent<ProjectileLinger>().lifetime = 4.0f;
            triggerLifetimeVfx = false;
        }
        */
    }

    public void OutroLocalScale(float elapsedTimeOutro)
    {
        if (cryBool == false)
        {
            BossCry();
            ObjectPool.Instance.GetAllPooledObjFromTagAndDeactivateBehaviour("BossProjectile");
        }
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(16, 16, 16), elapsedTimeOutro / durationTotalOutro);
        scaledIntensity = Mathf.Lerp(scaledIntensity, 15.0f, elapsedTimeOutro / durationTotalOutro);
        CinemachineCameraShake.Instance.CameraShakeOnSpecificCamera(scaledIntensity, 0.1f, cineMachineOutro);
    }

    public void SetIntroCamSetup(bool value)
    {
        introCamSetup = value;
    }

    private void BossCry()
    {
        audioSource.PlayOneShot(bossCrySound);
        cryBool = true;
    }
}
