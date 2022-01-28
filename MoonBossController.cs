using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FIMSpace.FTail;
using Cinemachine;
public class MoonBossController : MonoBehaviour
{
    public static MoonBossController Instance { get; private set; }
    [SerializeField] private Transform[] swoopWaypoints;
    [SerializeField] private Enemy moonBoss;
    [SerializeField] private Rigidbody rbBoss;
    private Transform player;
    [SerializeField] private LineRenderer moonLaser;
    [SerializeField] private Transform moonLaserRotateToPlayer, shadowPlayer, spawnPointCirclePattern, waypointVulnerable; //shadow player follows the player at any time
    [SerializeField] private Transform[] waypointArray;
    [SerializeField] private float bossSpeed, totemShieldAmount, rotateSpeed, timerToAlternateSeekPath, initTimerSeekPathAlternate, timerUntilSeekpath, projSpeed, dmgTresholdToStopSeekPath, dmgTresholdPhase2, dmgTresholdPhase3, rotationSpeed, timerUntilNewProjCircle, initTimerUntilCircle;
    [SerializeField] private GameObject dustVfxPrefab, shield;
    [SerializeField] private GameEvent shieldBreak, OnPhase2, OnFinishIntro;
    [SerializeField] private GameObject[] laserArray, totemArray;
    [SerializeField] private Transform anchorPoint, spawnPointSeekPathHolder;
    [SerializeField] private Vector3[] laserStart; // for the vector3 lerp of the laser phase;
    [SerializeField] private Vector3[] laserEnd; // for the vector3 lerp of the laser phase
    [SerializeField] private Transform[] spawnPointsSeekPathAlternate1, spawnPointsSeekPathAlternate2, spawnPointsTotem, spawnPointsTotemPhase2;
    [SerializeField] private bool isIntro, isSwitchingCam;
    private float elapsedTime = 0f;
    private float durationTotal = 3f;
    [SerializeField] private float timerUntilStart;
    private int currentWaypointIndex = 0;
    [SerializeField] private int maxBossHp, currentHp;
    public bool isVulnerable, isGrounded, isUsingLasersArray, alternateFireSeekPath, isUsingSeekPath, phase1, phase2, isMovingToVulnerable;
    private ObjectPool objPooler;
    [SerializeField] private bool laserDebug;
    public AudioSource audioSource;
    public AudioClip bossCrySound;
    [SerializeField] public CinemachineCamSwitch cmSwitch;
    private bool cryBool;
    private Vector3 initPos;
    [SerializeField] public TailAnimator2 tailAnim;
    public bool outroGameEventInvoke, isOutro;
    public GameEvent OnOutroStart;
    public CinemachineVirtualCamera cinemachineOutro;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        initPos = transform.position;
        for (int i = 0; i < laserArray.Length; i++)
        {
            laserArray[i].GetComponentInChildren<LineRenderer>().SetPosition(0, moonLaserRotateToPlayer.localPosition);
            laserArray[i].GetComponentInChildren<LineRenderer>().SetPosition(1, anchorPoint.localPosition);
        }
        cryBool = false;
        audioSource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        moonBoss = gameObject.GetComponent<Enemy>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        shadowPlayer = GameObject.FindGameObjectWithTag("Shadow").transform;
        objPooler = ObjectPool.Instance;
        maxBossHp = moonBoss.enemyHP;
        rbBoss = GetComponent<Rigidbody>();
        GetCurrentHp();
    }

    // Update is called once per frame
    void Update()
    {
        if (isIntro)
        {
            transform.position = initPos;
            timerUntilStart -= Time.deltaTime;
            if (timerUntilStart < 3.0f && isSwitchingCam)
            {
                cmSwitch.SwitchStateMenu("Boss2IntroCam2");
                isSwitchingCam = false;
            }
            if (timerUntilStart <= 0)
            {
                OnFinishIntro.Invoke();
            }
        }
        if (totemShieldAmount <= 0 && shield.activeInHierarchy)
        {
            shield.SetActive(false);
            moonBoss.hasShield = false;
            shieldBreak.Invoke();
        }
        if (!isOutro)
        {
            if (isVulnerable && isGrounded && !isUsingSeekPath && !isUsingLasersArray)
            {
                isUsingSeekPath = true;
            }
            if (isVulnerable && isGrounded && isUsingLasersArray)
            {
                /*
                if (elapsedTime < durationTotal)
                {
                    elapsedTime += Time.deltaTime;
                    spawnPointCirclePattern.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
                    CirclePattern();
                    //MoonLaserArrayDownToUp();
                }
                else
                {
                    elapsedTime = 0f;
                    transform.LookAt(player, transform.up);
                }
                */
                timerUntilNewProjCircle -= Time.deltaTime;
                if (timerUntilNewProjCircle <= 0)
                {
                    CirclePattern();
                    timerUntilNewProjCircle = initTimerUntilCircle;
                }
            }

            if (isVulnerable && isGrounded && isUsingSeekPath)
            {
                timerUntilSeekpath -= Time.deltaTime;
                if (timerUntilSeekpath <= 0)
                {
                    timerToAlternateSeekPath -= Time.deltaTime;
                    if (timerToAlternateSeekPath <= 0)
                    {
                        SeekPathPattern();
                        timerToAlternateSeekPath = initTimerSeekPathAlternate;
                        alternateFireSeekPath = !alternateFireSeekPath;
                        if (moonBoss.enemyHP <= currentHp * dmgTresholdToStopSeekPath / 100)
                        {
                            Debug.Log("LaserTime");
                            isUsingLasersArray = true;
                            //isUsingSeekPath = false;
                        }
                    }
                }
            }
        }



        if (moonBoss.enemyHP <= maxBossHp * dmgTresholdPhase2 / 100 && !phase2)
        {
            phase1 = false;
            phase2 = true;
            isUsingLasersArray = false;
            foreach (GameObject laser in laserArray)
            {
                laser.SetActive(false);
            }
            OnPhase2.Invoke();
            GetCurrentHp();
        }

        if (moonBoss.enemyHP <= 0 && cryBool == false)
        {
            bossCry();
        }
    }

    void LateUpdate()
    {
        if (laserDebug)
        {
            MoonLaserFollow(shadowPlayer);
        }
    }

    void FixedUpdate()
    {
        if (!isIntro)
        {
            if (!isVulnerable)
                Move();
            else if (isVulnerable && isMovingToVulnerable)
            {
                MoveToVulnerableSpot();
            }
            if (isGrounded && isVulnerable && !isUsingLasersArray && !isMovingToVulnerable)  //&& !isUsingLasersArray && !isUsingSeekPath)
            {
                transform.LookAt(player, transform.up);
            }

        }
        else
        {
            transform.position = initPos;
        }
        if (isUsingLasersArray)
            transform.Rotate(Vector3.up, rotationSpeed * Time.fixedDeltaTime);
    }

    private void SwoopPattern()
    {
        foreach (Transform waypoint in swoopWaypoints)
        {
            //if(Vector3.Distance(player.position, waypoint.position) < )
        }
    }

    private void MoonLaser()
    {
        moonLaser.SetPosition(0, transform.position);
        RaycastHit hit;
        Debug.DrawLine(moonLaserRotateToPlayer.position, player.transform.position, Color.green);
        if (Physics.Raycast(moonLaserRotateToPlayer.position, player.transform.position, out hit))
        {
            if (hit.collider.name == "Sphere_Projection")
            {
                Debug.Log(hit.collider.name);
                moonLaser.SetPosition(1, hit.point);
                Instantiate(dustVfxPrefab, hit.point, Quaternion.identity);
            }
            else
            {

            }
        }
    }

    private void CirclePattern()
    {
        GameObject projectileClone = objPooler.SpawnFromPool("MoonBossProj", spawnPointCirclePattern.position, spawnPointCirclePattern.rotation);
        ProjectileManager projManager = projectileClone.GetComponent<ProjectileManager>();
        projManager.timerUntilDestroyed = 7.0f;
        projManager.isSpawnedBySpawner = true;
        projManager.randomDirection = Vector3.forward;
        projManager.randomProjSpeed = projSpeed * 3f;
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypointArray[currentWaypointIndex].position, bossSpeed * Time.fixedDeltaTime);
        //Vector3 targetDirection = waypointArray[currentWaypointIndex].position - transform.position;
        //Vector3 newLookDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotateSpeed * Time.fixedDeltaTime, 0.0f);
        //Debug.DrawRay(transform.position, targetDirection, Color.cyan);
        //Debug.DrawRay(transform.position, newLookDirection * 100f, Color.yellow);
        //transform.rotation = Quaternion.LookRotation(newLookDirection);
        transform.LookAt(waypointArray[currentWaypointIndex], transform.up);
        Debug.DrawRay(transform.position, transform.position.normalized * 100f, Color.red);

        if (Vector3.Distance(transform.position, waypointArray[currentWaypointIndex].position) <= 2.0f)
        {
            currentWaypointIndex += 1;

            if (currentWaypointIndex == waypointArray.Length)
            {
                currentWaypointIndex = 0;
            }
        }
    }

    void MoveToVulnerableSpot()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypointVulnerable.position, (bossSpeed * 3) * Time.fixedDeltaTime);
        transform.LookAt(waypointVulnerable, transform.up);
        if (Vector3.Distance(transform.position, waypointVulnerable.position) <= 2.0f)
        {
            isMovingToVulnerable = false;
            spawnPointSeekPathHolder.parent = null;
            currentWaypointIndex = 0;
        }
    }

    public void SubstractShield(int amount)
    {
        totemShieldAmount -= amount;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
            GameObject dustClone = Instantiate(dustVfxPrefab, transform.position, Quaternion.identity);
            dustClone.transform.localScale = new Vector3(8, 8, 8);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = false;
            spawnPointSeekPathHolder.transform.parent = this.transform;
            spawnPointSeekPathHolder.transform.localPosition = Vector3.zero;
            spawnPointSeekPathHolder.transform.localRotation = new Quaternion(0, 0, 0, 0);
            Move();
        }
    }

    public void SetTotemShieldAmount(int amount)
    {
        totemShieldAmount = amount;
        shield.SetActive(true);
        moonBoss.hasShield = true;
    }

    public void SetIsVulnerable(bool newValue)
    {
        isVulnerable = newValue;
    }
    public void SetIsMovingtoVulnerable(bool newValue)
    {
        isMovingToVulnerable = newValue;
    }

    private void MoonLaserFollow(Transform entityToFollow)
    {
        moonLaser.SetPosition(0, moonLaserRotateToPlayer.position);
        moonLaser.SetPosition(1, entityToFollow.position);
    }

    private void MoonLaserArrayDownToUp()
    {
        for (int i = 0; i < laserArray.Length; i++)
        {
            laserArray[i].SetActive(true);
            laserArray[i].GetComponentInChildren<LineRenderer>().SetPosition(0, moonLaserRotateToPlayer.localPosition);
            laserArray[i].GetComponentInChildren<LineRenderer>().SetPosition(1, anchorPoint.localPosition);
            laserArray[i].GetComponentInChildren<LineRenderer>().SetPosition(2, Vector3.Lerp(laserStart[i], laserEnd[i], elapsedTime / durationTotal));
        }
    }

    // 65 à -20

    private void LaserArrayPattern()
    {

    }

    private void SeekPathPattern()
    {
        if (!alternateFireSeekPath)
        {
            foreach (Transform spawnPointSeek in spawnPointsSeekPathAlternate1)
            {
                GameObject projectileClone = objPooler.SpawnFromPool("MoonBossProj", spawnPointSeek.position, spawnPointSeek.rotation);
                ProjectileManager projManager = projectileClone.GetComponent<ProjectileManager>();
                projManager.timerUntilDestroyed = 15.0f;
                projManager.isSpawnedBySpawner = true;
                projManager.randomDirection = Vector3.forward;
                projManager.randomProjSpeed = projSpeed;
            }
        }
        else
        {
            foreach (Transform spawnPointSeek in spawnPointsSeekPathAlternate2)
            {
                GameObject projectileClone = objPooler.SpawnFromPool("MoonBossProj", spawnPointSeek.position, spawnPointSeek.rotation);
                ProjectileManager projManager = projectileClone.GetComponent<ProjectileManager>();
                projManager.timerUntilDestroyed = 15.0f;
                projManager.isSpawnedBySpawner = true;
                projManager.randomDirection = Vector3.forward;
                projManager.randomProjSpeed = projSpeed;
            }
        }
    }

    public void SpawnTotems()
    {
        for (int i = 0; i < totemArray.Length; i++)
        {
            if (phase1)
            {
                GameObject totemClone = Instantiate(totemArray[i], spawnPointsTotem[i].position, spawnPointsTotem[i].rotation);
                totemClone.GetComponent<TotemMoonBossController>().isRed = true;

            }
            if (phase2)
            {
                GameObject totemClone = Instantiate(totemArray[i], spawnPointsTotemPhase2[i].position, spawnPointsTotemPhase2[i].rotation);
                totemClone.GetComponent<TotemMoonBossController>().isWhite = true;
                totemClone.GetComponent<TotemMoonBossController>().isRed = false;
            }
        }
    }

    public void GetCurrentHp()
    {
        currentHp = moonBoss.enemyHP;
    }
    public void BossImpulse() // to get out of the ground after each ground phase
    {
        rbBoss.AddForce(transform.up * 2);
        rbBoss.velocity = Vector3.zero;
        SetIsVulnerable(false);
        rbBoss.velocity = Vector3.zero;
        rbBoss.velocity = Vector3.zero;
        rbBoss.velocity = Vector3.zero;
        rbBoss.velocity = Vector3.zero;
        rbBoss.velocity = Vector3.zero;
        rbBoss.velocity = Vector3.zero;
        rbBoss.velocity = Vector3.zero;
        rbBoss.velocity = Vector3.zero;
        rbBoss.velocity = Vector3.zero;
        rbBoss.velocity = Vector3.zero;
        rbBoss.velocity = Vector3.zero;
        rbBoss.velocity = Vector3.zero;
        rbBoss.velocity = Vector3.zero;
        rbBoss.velocity = Vector3.zero;
    }

    private void bossCry()
    {
        audioSource.PlayOneShot(bossCrySound);
        cryBool = true;
    }

    /* Boss look at player lors de phase seek path
     * Totem spawner differents
     * Animation de mort totem
     * Spawn et dspawn de mines
     * colonne de flammes lors de phase volante
     * Switch camera player pour plus lisibilite
     * 
     */

    public void SetIsIntro(bool value)
    {
        isIntro = value;
    }

    public void SetInitTimerCircle(float value)
    {
        initTimerUntilCircle = value;
    }

    public void SetAlternateSeekPathInitTimer(float value)
    {
        initTimerSeekPathAlternate = value;
    }

    public void OutroMoonBoss()
    {
        tailAnim.enabled = true;
        isUsingLasersArray = false;
        isUsingSeekPath = false;
        isOutro = true;
        objPooler.GetAllPooledObjFromTagAndDeactivateBehaviour("MoonBossProj");
    }
}
