using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem.UI;
using UnityEngine.InputSystem;
using Cinemachine;
using System.Linq;
using FIMSpace.FTail;
public class PlayerMovement : MonoBehaviour
{

    [Header("Movement Related")]
    [Range(5.0f, 35.0f)]
    [SerializeField] public float speed;
    [SerializeField] private float knockBackStrength;
    [SerializeField] private float exploModeSpeedMod;
    [SerializeField] private float accelerateAxis;
    [SerializeField] public float rotateSpeed;
    [SerializeField] public PlayerActionControls playerActions;
    [SerializeField] public Rigidbody PlayerRb;
    [SerializeField] private float xMovAxis;
    [SerializeField] private float zMovAxis;
    [SerializeField] public float triggerStatueValue;
    [SerializeField] private float triggerScanValue;
    [SerializeField] private float triggerCombatModeValue;
    [SerializeField] private bool combatMode = true;
    [SerializeField] private bool cursorON = false;
    [Range(75.0f, 250.0f)]
    [SerializeField] public float joystickAxisSensitivity;
    [SerializeField] public float YjoystickAxisSensitivity;
    [SerializeField] private Vector3 movementDir;
    [SerializeField] private Vector3 targetMoveAmount;
    [SerializeField] private Vector3 moveAmount;
    [SerializeField] private Vector3 smoothMoveVelocity;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;
    [SerializeField] private float initDashTime;
    [SerializeField] private float invulAfterDash;
    [SerializeField] private float triggerDashValue;
    [SerializeField] private float dashCooldown;
    [SerializeField] private float initDashCooldown;
    [SerializeField] private bool dashActive;
    [SerializeField] private GameObject dashCheck;
    [SerializeField] private Material dashActiveMat;
    [SerializeField] private GameObject ScanSystem;
    [SerializeField] private GameObject ScanSystemDecal;
    [SerializeField] private GameObject ScanSystemFX;
    [SerializeField] private float timerToAddEnemyToScan;
    [SerializeField] private int maxTargets;
    [SerializeField] private Material scanActiveMat;
    [SerializeField] private Material scanUnactiveMat;
    [SerializeField] private GameObject scanCheck;
    [SerializeField] private PlayerUIController playerUI;
    [SerializeField] private GameObject bombControl;
    [SerializeField] private int xpValueOfPickUp;
    [SerializeField] private int hpValueOfPickUp;
    [SerializeField] public int currentHpValue;
    public int currentXpValue;
    [SerializeField] private int maxHpValue;
    [SerializeField] private float triggerBombValue;
    [SerializeField] private GameObject dashVFXPrefab, healVFXPrefab, xpVFXPrefab;

    [SerializeField] private float duration;
    bool _isFrozen = false;
    float _pendingFreezeDuration = 0.0f;

    [Header("Camera Related")]
    [SerializeField] public Camera maincam;
    [SerializeField] public CinemachineVirtualCamera cmCam;
    [SerializeField] public Transform camPivot;
    [SerializeField] public float pivotSpeed;
    [SerializeField] private Vector3 maincamDefaultPos;
    [SerializeField] public float smoothCamSpeed;
    [SerializeField] public float camDivideFactor;
    [SerializeField] private float combatModeFOV;
    [SerializeField] private float exploModeFOV;
    [SerializeField] private float timeToLerpToExplo;
    [SerializeField] private float timeToLerpToCombat, shakeIntensityOnhit, shakeTimeOnHit;
    [SerializeField] private bool fovTransitionInitialize;

    [Header("Cursor Related")]
    [SerializeField] private float xMovCursorAxis;
    [SerializeField] private float yMovCursorAxis;
    //[SerializeField] private VirtualMouseInput mouseInput;

    [Header("Virus Level")]
    //Value to change on layer switch
    public Transform actualVirusLayer;
    [Header("Audio Related")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hitPlayer;
    [SerializeField] private AudioClip pickUp;
    [SerializeField] private AudioClip levelUp;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private bool shootSoundOnce = true;

    [Header("DebugValues")]
    [SerializeField] private Text hitCounterText;
    [SerializeField] public int timesHit;
    [SerializeField] public float invulframes;
    [SerializeField] private float initInvulFrames, initAnemoneTimerHaptics, anemoneTimerHaptics;
    [SerializeField] private Material invulCheckMat;
    [SerializeField] private GameObject invulCheck;
    [SerializeField] private PlayerValues PlayerLevelValues;
    [Range(1, 3)]
    [SerializeField] private int playerLevelDebug;
    [SerializeField] private LevelChanger levelChanger;
    [SerializeField] private GameController gameController;
    [SerializeField] public bool LockAllInputs, anemoneCheckHaptics;
    [SerializeField] private Animator deathAnimator;
    [SerializeField] private TailAnimator2 leftTail, rightTail;

    [Header("Enemy Projectiles Spawn")]
    [SerializeField] private GameObject enemyProjPrefab;
    [SerializeField] private int enemyCount = 0;
    [SerializeField] public List<Enemy> enemyList;

    [HideInInspector]
    public Mouse mouse;
    private PlayerInput _playerInput;
    private Gamepad gamepad;
    private bool hapticsOn, hapticsGameEventOn;
    private float timerHapticsEvent;
    //Instantiate playerActions as a new PlayerActionControls which is the class that holds all inputs for the player.

    private void Awake()
    {
        mouse = Mouse.current;
        Cursor.visible = false;
        _playerInput = gameObject.GetComponent<PlayerInput>();
        playerActions = new PlayerActionControls();
        maincamDefaultPos = maincam.transform.localPosition;
        //mouseInput = GameObject.FindObjectOfType<VirtualMouseInput>();
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        ScanSystem.SetActive(false);
        ScanSystemDecal.SetActive(false);
        timerToAddEnemyToScan = PlayerLevelValues.lockTimerToAddEnemyToScan;
        maxTargets = 0;
        playerUI = FindObjectOfType<PlayerUIController>();
        maxHpValue = PlayerLevelValues.maxHpValue;
        playerUI.SetMaxHpValue(maxHpValue, currentHpValue);
        dashTime = initDashTime;
        dashCooldown = PlayerLevelValues.dashCD;
        PlayerLevelValues.LevelUp(playerLevelDebug);
        levelChanger = FindObjectOfType<LevelChanger>();
        gameController = FindObjectOfType<GameController>();
        initAnemoneTimerHaptics = anemoneTimerHaptics;
    }

    //Enables the action map, so we can read values from inputs.
    private void OnEnable()
    {
        playerActions.Enable();
    }

    private void OnDisable()
    {
        playerActions.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        leftTail.enabled = true;
        rightTail.enabled = true;
        PlayerRb = GetComponent<Rigidbody>();
        ScanSystemFX.SetActive(false);
        audioSource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!LockAllInputs)
        {
            gamepad = GetGamepad();
            //xMovAxis = Input.GetAxisRaw("Horizontal");
            //zMovAxis = Input.GetAxisRaw("Vertical");

            //Read the value from the action map, either from gamepad or keyboard, return a float to parse in the vector
            xMovAxis = playerActions.OnVirus.MoveSideways.ReadValue<float>();
            zMovAxis = playerActions.OnVirus.MoveForward.ReadValue<float>();

            xMovCursorAxis = playerActions.OnVirus.CursorHorizontalControl.ReadValue<float>();
            //yMovCursorAxis = playerActions.OnVirus.CursorVerticalControl.ReadValue<float>();

            triggerScanValue = playerActions.OnVirus.TriggerScan.ReadValue<float>();
            triggerBombValue = playerActions.OnVirus.TriggerBomb.ReadValue<float>();
            triggerDashValue = playerActions.OnVirus.TriggerDash.ReadValue<float>();
            triggerCombatModeValue = playerActions.OnVirus.TriggerCombatMode.ReadValue<float>();
            accelerateAxis = playerActions.OnVirus.Accelerate.ReadValue<float>();
            triggerStatueValue = playerActions.OnVirus.TriggerStatue.ReadValue<float>();


            //Check interactions : Performed value callback context. inputSystem


            if (triggerScanValue > 0 && combatMode)
            {
                shootSoundOnce = true;
                ScanSystem.SetActive(true);
                ScanSystemDecal.SetActive(true);
                ScanSystemFX.SetActive(true);
                ScanSystem.GetComponent<WeaponSystem>().canShoot = false;
                scanCheck.GetComponent<Renderer>().material = scanActiveMat;
                timerToAddEnemyToScan -= Time.deltaTime;
                if (timerToAddEnemyToScan <= 0 && ScanSystem.GetComponent<WeaponSystem>().enemyList.Count != 0)
                {
                    //ChangeAnimationState(PLAYER_SCAN);
                    ScanSystem.GetComponent<WeaponSystem>().SalvoScan();
                    timerToAddEnemyToScan = PlayerLevelValues.lockTimerToAddEnemyToScan;
                }
                //cursorON = true;
                //mouseInput.GetComponent<Image>().enabled = true;
                //mouseInput.enabled = true;           

            }

            else
            {
                if (ScanSystem.activeSelf)
                {
                    ScanSystem.GetComponent<WeaponSystem>().canShoot = true;
                    timerToAddEnemyToScan = PlayerLevelValues.lockTimerToAddEnemyToScan;
                    maxTargets = 0;
                    //ChangeAnimationState(PLAYER_SHOOT);
                    //isScanning = false;               
                }
                
                scanCheck.GetComponent<Renderer>().material = scanUnactiveMat;
                cursorON = false;
                ScanSystemDecal.SetActive(false);
                ScanSystemFX.SetActive(false);
                //mouseInput.GetComponent<Image>().enabled = false;
                //mouseInput.enabled = false;            
            }

            if (triggerScanValue == 0 && shootSoundOnce && ScanSystem.GetComponent<WeaponSystem>().enemyList.Count != 0)
            {
                audioSource.PlayOneShot(shootSound);
                shootSoundOnce = false;
            }

            if (combatMode)
            {
                //.normalized for a vector of 1 ( means you can't move slowly with the joystick )
                movementDir = new Vector3(xMovAxis, 0, zMovAxis);
                targetMoveAmount = movementDir * speed;
                moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, 0.05f);
            }

            else
            {
                movementDir = new Vector3(0, 0, triggerCombatModeValue);
                targetMoveAmount = movementDir * (speed + exploModeSpeedMod);
                moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, 0.35f);
            }



            if (currentHpValue > maxHpValue)
            {
                currentHpValue = maxHpValue;
            }

            /* Fonctionnement de la bombe : invalide, besoin de clean !
            if (currentHpValue == maxHpValue && triggerBombValue > 0)
            {
                bombControl.gameObject.SetActive(true);
                currentHpValue = 0;
                bombBar.SetBombValue(currentHpValue);
            }
            */
            if (dashCooldown >= 0)
            {
                dashCooldown -= Time.deltaTime;
                dashCheck.GetComponent<Renderer>().material = scanActiveMat;
            }
            else if (dashCooldown <= 0)
            {
                dashCheck.GetComponent<Renderer>().material = dashActiveMat;
            }
            //SpawnEnemyProjectiles();
            if (invulframes >= 0)
            {
                invulCheck.SetActive(true);
                invulCheck.GetComponent<Renderer>().material = invulCheckMat;
            }
            else
            {
                invulCheck.SetActive(false);
                //invulCheck.GetComponent<Renderer>().material = scanActiveMat;
            }
            if (invulframes <= 1.75f)
            {
                if (hapticsOn)
                {
                    if (gamepad != null)
                    {
                        gamepad.SetMotorSpeeds(0f, 0f);
                    }
                    hapticsOn = false;
                }
            }
            if (timerHapticsEvent <= 0)
            {
                if (hapticsGameEventOn)
                {
                    if (gamepad != null)
                    {
                        gamepad.SetMotorSpeeds(0f, 0f);
                    }
                    hapticsGameEventOn = false;
                }
            }

            if (timerHapticsEvent >= 0)
                timerHapticsEvent -= Time.deltaTime;

            invulframes -= Time.deltaTime;

            //-------------Combat Mode Control, uncomment all to enable---------------
            
            /*
            playerActions.OnVirus.TriggerCombatMode.started +=
                context =>
                {
                    fovTransitionInitialize = true;
                };

            playerActions.OnVirus.TriggerCombatMode.canceled +=
                context =>
                {
                    fovTransitionInitialize = true;
                };

            
            if (triggerCombatModeValue > 0)
                combatMode = false;
            else
                combatMode = true;
            
            if (combatMode)
            {
                cmCam.m_Lens.FieldOfView = Mathf.Lerp(maincam.fieldOfView, combatModeFOV, timeToLerpToCombat * Time.deltaTime);
            }
            else
            {
                //cmCam.m_Lens.FieldOfView = Mathf.Lerp(maincam.fieldOfView, exploModeFOV, timeToLerpToExplo * Time.deltaTime);
            }
            */
            if (_pendingFreezeDuration > 0 && !_isFrozen)
            {
                StartCoroutine(Dofreeze());
            }
            /*
            //Conditions for animation states
            if(!isScanning)
            {
                if (zMovAxis < -0.1f)
                {
                    ChangeAnimationState(PLAYER_BACKWARD);
                }

                if (zMovAxis > 0.1f)
                {
                    ChangeAnimationState(PLAYER_FORWARD);
                }

                if (xMovAxis < -0.3f && zMovAxis < 0.5f && zMovAxis > -0.5f)
                {
                    ChangeAnimationState(PLAYER_LEFT);
                }

                else if (xMovAxis > 0.3f && zMovAxis < 0.5f && zMovAxis > -0.5f)
                {
                    ChangeAnimationState(PLAYER_RIGHT);
                }
            }
            */
            if (gameController == null)
            {
                gameController = FindObjectOfType<GameController>();
            }
            if (currentHpValue <= 0)
            {
                deathAnimator.enabled = true;
                levelChanger.hasDied = true;
                gameController.hasDied = true;
            }
            if(anemoneCheckHaptics)
            {
                anemoneTimerHaptics -= Time.deltaTime;
                if(anemoneTimerHaptics <= 0)
                {
                    anemoneCheckHaptics = false;
                }
            }
        }
        else if(LockAllInputs)
        {
            PlayerRb.velocity = Vector3.zero;
            moveAmount = Vector3.zero;
        }
    }

    void FixedUpdate()
    {

        //Camera stuff si cinemachine inutile, décommenter ces 3 lignes.
        /*
        Vector3 CameraMovement = new Vector3(maincamDefaultPos.x + (xMovAxis / camDivideFactor), maincamDefaultPos.y, maincamDefaultPos.z + (zMovAxis / camDivideFactor));
        Vector3 CameraSmoothMovement = Vector3.Lerp(maincamDefaultPos, CameraMovement, smoothCamSpeed * Time.fixedDeltaTime);
        maincam.transform.localPosition = CameraSmoothMovement;
        */


        RotateCamera();
        Move();
        //-----------Dash Control, uncomment all to activate-----------
        /*
        if (triggerDashValue > 0 && !dashActive && dashCooldown <= 0)
        {
            dashActive = true;
            GameObject dashVfxClone = Instantiate(dashVFXPrefab, transform.position, transform.rotation);
        }
        else if (dashActive)
        {
            Dash();
        }
        else if (!dashActive)
        {
            Move();
        }*/

    }

    void Move()
    {
        PlayerRb.MovePosition(PlayerRb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
        /* rb.AddForce((rb.position - actualVirusLayer.position).normalized * (gravity / 10));
         transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(transform.up,
             (transform.position - actualVirusLayer.position).normalized) * transform.rotation, rotateSpeed); */
    }

    void Dash()
    {
        if (invulframes <= initDashTime)
        {
            invulframes = initDashTime + invulAfterDash;
        }

        dashTime -= Time.deltaTime;
        PlayerRb.velocity = transform.TransformDirection(moveAmount) * dashSpeed;
        if (dashTime <= 0)
        {
            dashTime = initDashTime;
            dashActive = false;
            PlayerRb.velocity = Vector3.zero;
            dashCooldown = PlayerLevelValues.dashCD;
        }
    }

    void RotateCamera()
    {

        //VectorUp : Y Axis, left - right rotation, horitonzal input.
        //camPivot.Rotate(Vector3.up * xMovCursorAxis * Time.fixedDeltaTime * joystickAxisSensitivity, Space.Self);
        //a locker lors de la phase de lock-on freeze camera par un bool
        if (combatMode)
        {
            camPivot.Rotate(Vector3.right * -yMovCursorAxis * Time.fixedDeltaTime * YjoystickAxisSensitivity, Space.Self);
            transform.Rotate(Vector3.up * xMovCursorAxis * Time.fixedDeltaTime * joystickAxisSensitivity, Space.Self);
            camPivot.transform.localEulerAngles = new Vector3(camPivot.transform.localEulerAngles.x, 0, 0);
        }
        else
        {
            //camPivot.Rotate(Vector3.right * xMovAxis * Time.fixedDeltaTime * YjoystickAxisSensitivity, Space.Self);
            transform.Rotate(Vector3.up * xMovAxis * Time.fixedDeltaTime * joystickAxisSensitivity, Space.Self);
            camPivot.transform.localEulerAngles = new Vector3(camPivot.transform.localEulerAngles.x, 0, 0);
        }

    }

    /*
    void SpawnHomingProjectiles()
    {

        Ray ray = Camera.main.ScreenPointToRay(mouseInput.virtualMouse.position.ReadValue());
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            if(hit.collider.tag == "Enemy")
            {
                
            }
        }

    }
    */

    //WIP Objectif, si ennemi avec beaucoup de points de vie, si on s'éloigne, on s'arrête de tirer dessus.
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HPUpgrade")
        {
            Destroy(other.gameObject);
            currentHpValue += hpValueOfPickUp;
            playerUI.SetHpValue(currentHpValue);
            Instantiate(healVFXPrefab, transform);
            audioSource.PlayOneShot(pickUp);
        }

        if (other.gameObject.tag == "XPUpgrade")
        {
            Destroy(other.gameObject);
            currentXpValue += xpValueOfPickUp;
            playerUI.SetXpValue(currentXpValue);
            Instantiate(xpVFXPrefab, transform);
            audioSource.PlayOneShot(levelUp);
        }

        if (other.gameObject.tag == "Spawner")
        {
            other.GetComponent<SpawnerFollowPlayer>().enabled = true;
        }

        if (other.gameObject.tag == "FireColumn")
        {
            if (invulframes <= 0)
            {
                audioSource.PlayOneShot(hitPlayer);
                timesHit++;
                hitCounterText.text = "Times Hit : " + timesHit;
                invulframes = initInvulFrames;
                currentHpValue -= 20;
                playerUI.SetHpValue(currentHpValue);
                playerUI.TriggerScreenHit();
                CinemachineCameraShake.Instance.CameraShake(shakeIntensityOnhit, shakeTimeOnHit);
                if (gamepad != null)
                    gamepad.SetMotorSpeeds(0.15f, 0.35f);
                hapticsOn = true;
                other.gameObject.SetActive(false);
            }
        }
        if(other.gameObject.tag == "Anemone")
        {
            if(invulframes <= 0 && !hapticsOn && !anemoneCheckHaptics)
            {
                //StartSpecificHaptics(0.075f, 0f, 0.0125f);
            }
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (invulframes <= 0)
            {
                audioSource.PlayOneShot(hitPlayer);
                Debug.Log(PlayerRb);
                if (PlayerRb != null)
                {
                    Vector3 direction = transform.position - collision.transform.position;
                    //direction.y = transform.up.y;
                    PlayerRb.AddForce(direction.normalized * knockBackStrength, ForceMode.Impulse);
                }
                if (collision.gameObject.name == "HomingBossMissile(Clone)" || collision.gameObject.name == "EnemyFollowPlayer_BeholderVariant(Clone)" || collision.gameObject.name == "EnemyFollowPlayer(Clone)")
                {
                    Destroy(collision.gameObject);
                }
                timesHit++;
                hitCounterText.text = "Times Hit : " + timesHit;
                invulframes = initInvulFrames;
                currentHpValue -= 20;
                playerUI.SetHpValue(currentHpValue);
                playerUI.TriggerScreenHit();
                CinemachineCameraShake.Instance.CameraShake(shakeIntensityOnhit, shakeTimeOnHit);
                if (gamepad != null)
                    gamepad.SetMotorSpeeds(0.15f, 0.35f);
                hapticsOn = true;
            }

        }

        if (collision.gameObject.tag == "EnemyProjectile")
        {
            if (invulframes <= 0)
            {
                audioSource.PlayOneShot(hitPlayer);
                timesHit++;
                hitCounterText.text = "Times Hit : " + timesHit;
                invulframes = initInvulFrames;
                currentHpValue -= 20;
                playerUI.SetHpValue(currentHpValue);
                playerUI.TriggerScreenHit();
                CinemachineCameraShake.Instance.CameraShake(shakeIntensityOnhit, shakeTimeOnHit);
                if (gamepad != null)
                    gamepad.SetMotorSpeeds(0.15f, 0.35f);
                hapticsOn = true;
            }
        }
        if (collision.gameObject.tag == "CristalShard")
        {
            if (invulframes <= 0)
            {
                audioSource.PlayOneShot(hitPlayer);
                timesHit++;
                hitCounterText.text = "Times Hit : " + timesHit;
                invulframes = initInvulFrames;
                currentHpValue -= 20;
                playerUI.SetHpValue(currentHpValue);
                playerUI.TriggerScreenHit();
                CinemachineCameraShake.Instance.CameraShake(shakeIntensityOnhit, shakeTimeOnHit);
                if (gamepad != null)
                    gamepad.SetMotorSpeeds(0.15f, 0.35f);
                hapticsOn = true;
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Anemone")
        {
            anemoneCheckHaptics = true;
            anemoneTimerHaptics = initAnemoneTimerHaptics;
        }
    }

    private void Freeze()
    {
        _pendingFreezeDuration = duration;
    }

    IEnumerator Dofreeze()
    {
        _isFrozen = true;
        var original = Time.timeScale;
        Time.timeScale = 0.6f;

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = original;
        _pendingFreezeDuration = 0.0f;
        _isFrozen = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Spawner")
        {
            other.GetComponent<SpawnerFollowPlayer>().enabled = false;
        }
    }

    private Gamepad GetGamepad()
    {
        /*Gamepad gamepad = null;
        foreach (Gamepad g in Gamepad.all)
        {
            foreach (InputDevice d in playerActions.devices)
            {
                if (d.deviceId == g.deviceId)
                {
                    gamepad = g;
                    break;
                }

            }
            if (gamepad == null)
            {
                break;
            }
        }
        return gamepad;
        */
        return Gamepad.all.FirstOrDefault(g => _playerInput.devices.Any(d => d.deviceId == g.deviceId));
    }
    //WIP

    public void StartHaptics(float time)
    {
        timerHapticsEvent = time;
        hapticsGameEventOn = true;
        if (gamepad != null)
            gamepad.SetMotorSpeeds(0.25f, 0.45f);
    }
    public void StartRoughHaptics(float time)
    {
        timerHapticsEvent = time;
        hapticsGameEventOn = true;
        if (gamepad != null)
            gamepad.SetMotorSpeeds(0.55f, 0.75f);
    }

    public void StartSpecificHaptics(float time, float leftMotorSpeed, float rightMotorSpeed)
    {
        timerHapticsEvent = time;
        hapticsGameEventOn = true;
        if (gamepad != null)
            gamepad.SetMotorSpeeds(leftMotorSpeed, rightMotorSpeed);
    }

    public void SetLockPlayerInput(bool value)
    {
        LockAllInputs = value;
    }
}
