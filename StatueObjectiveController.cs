using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;
public class StatueObjectiveController : MonoBehaviour
{
    private PlayerMovement player;
    private bool activateEvent, bossDefeated, startTimer, spawnVFXDone;
    private PlayerUIController playerUI;
    [SerializeField] private bool isGameEventTied;
    [SerializeField] private Text infoText;
    [SerializeField] private Image BackgroundInfoText;
    [SerializeField] private Text statueObjective;
    [SerializeField] private GameObject bossPrefab, enemySpawnVFXPrefab, wall1, wall2, xpSpawner, lockUpTextGO;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private string statueObjectiveDescription;
    [SerializeField] private VisualEffect ActivateStatue;
    [SerializeField] GameEvent OnEventStart, OnEventEnd;
    private GameObject bossInstantiated, enemySpawnVFXClone;
    private float timerToSpawnBoss = 1.0f;
    public int indexForGameController;
    public bool isNotSecondary;
    public bool noSpawnBossInCode;
    private bool bossHeartSet;
    private GameObject bossHeart;

    [Header("Audio Related")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip activationSound;

    void Awake()
    {
        activateEvent = false;
        infoText.gameObject.SetActive(false);
        BackgroundInfoText.gameObject.SetActive(false);
        player = FindObjectOfType<PlayerMovement>();
        playerUI = FindObjectOfType<PlayerUIController>();
        spawnVFXDone = false;
        if(statueObjective.GetComponent<Animator>() != null)
            statueObjective.GetComponent<Animator>().enabled = false;
        ActivateStatue = GetComponentInChildren<VisualEffect>();
        audioSource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
    }
    void Start()
    {
        wall1.SetActive(false);
        wall2.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (bossInstantiated == null && activateEvent == true && spawnVFXDone)
        {
            bossDefeated = true;
            
            if(isNotSecondary)
            {
                if (!bossHeartSet)
                {
                    bossHeart = GameObject.FindGameObjectWithTag("XPUpgrade");
                    bossHeartSet = true;
                }
                if (bossHeartSet)
                {
                    if(bossHeart == null)
                    {
                        lockUpTextGO.SetActive(true);
                        Destroy(this.gameObject);

                        if (wall2.GetComponent<Animator>() != null)
                        {
                            wall2.GetComponent<Animator>().enabled = true;
                            wall2.GetComponent<Animator>()?.Play("Dissolve_Door");
                        }
                    }
                }
            }
            if(!isNotSecondary)
            {
                Destroy(this.gameObject);

                if (wall2.GetComponent<Animator>() != null)
                {
                    wall2.GetComponent<Animator>().enabled = true;
                    wall2.GetComponent<Animator>()?.Play("Dissolve_Door");
                }
            }
            //wall1.SetActive(false); 
        }
        if (bossDefeated)
        {
            statueObjective.color = Color.green;
        }
        if (startTimer && !spawnVFXDone)
        {
            timerToSpawnBoss -= Time.deltaTime;
        }
        if (timerToSpawnBoss <= 0 && !spawnVFXDone)
        {
            if (!isGameEventTied)
            {
                bossInstantiated = Instantiate(bossPrefab, spawnPoint.position, Quaternion.identity);
            }
            else if (isGameEventTied)
            {
                bossInstantiated = bossPrefab;
                if (!noSpawnBossInCode)
                {
                    bossPrefab.SetActive(true);
                }
            }
            spawnVFXDone = true;
        }
        if (activateEvent && spawnVFXDone)
        {
            if (!isGameEventTied)
            {
                bossInstantiated.transform.position = spawnPoint.position;
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && !activateEvent)
        {
            infoText.gameObject.SetActive(true);
            infoText.text = "Press X to start event";
            BackgroundInfoText.gameObject.SetActive(true);
            if (player.triggerStatueValue > 0)
            {
                OnEventStart?.Invoke();
                wall1.SetActive(true);
                infoText.gameObject.SetActive(false);
                BackgroundInfoText.gameObject.SetActive(false);
                activateEvent = true;
                statueObjective.text = statueObjectiveDescription;
                if (statueObjective.GetComponent<Animator>() != null)
                {
                    statueObjective.GetComponent<Animator>().enabled = true;
                    statueObjective.GetComponent<Animator>()?.Play("BossTextAnimation");
                }
                startTimer = true;
                ActivateStatue.enabled = true;
                audioSource.PlayOneShot(activationSound);
                if (isNotSecondary && enemySpawnVFXClone != null)
                {
                    enemySpawnVFXClone = Instantiate(enemySpawnVFXPrefab, spawnPoint.position, Quaternion.identity);
                    enemySpawnVFXClone.GetComponent<ProjectileLinger>().enabled = true;
                    enemySpawnVFXClone.GetComponent<ProjectileLinger>().enableLingeringEffect = true;
                    enemySpawnVFXClone.transform.LookAt(player.gameObject.GetComponent<Transform>().transform, player.gameObject.GetComponent<Transform>().transform.up);
                    enemySpawnVFXClone.transform.localScale = new Vector3(6, 6, 6);
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && !activateEvent)
        {
            infoText.gameObject.SetActive(false);
            BackgroundInfoText.gameObject.SetActive(false);
        }
    }

    void OnDestroy()
    {
        if (GameController.Instance != null && isNotSecondary)
        {
            playerUI.objectivesCleared += 1;
            //playerUI.FillObjective();
            playerUI.FillTotemImage(indexForGameController);
            playerUI.CheckObjectiveClear();
            GameController.Instance.OnStatueObjectiveDestroyed(this);
            if (isGameEventTied)
            {
                xpSpawner?.SetActive(true);
            }
        }

        if (!isNotSecondary)
        {
            xpSpawner?.SetActive(true);
            OnEventEnd?.Invoke();
        }

    }

}
