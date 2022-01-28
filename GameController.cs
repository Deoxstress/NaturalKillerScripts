using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int targetFrameRate = 60;
    private GameObject player;
    public Vector3 lastKnownPlayerPosition;
    public Quaternion lastKnownPlayerRot;
    public float timerToSavePos = 10.0f;
    public float timerUntilRespawn = 1.15f;
    private float initTimer, initRespawn;
    public bool hasDied = false;
    public bool[] objectivesIndex; // Objectives tied to a statue   
    [SerializeField] private List<StatueObjectiveController> statuesObjective;
    [SerializeField] private int objectivesCompleted;
    [SerializeField] private PlayerUIController playerUI;

    static GameController instance;
    public static GameController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<GameController>();
            }
            return instance;
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Gamecontroller");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    public void Start()
    {

        //QualitySettings.vSyncCount = 0;
        statuesObjective.Clear();
        Application.targetFrameRate = targetFrameRate;
        player = GameObject.FindGameObjectWithTag("Player");
        playerUI = FindObjectOfType<PlayerUIController>();
        AddStatuesToList();
        if (hasDied)
        {
            Respawn();
        }
        initTimer = timerToSavePos;
        initRespawn = timerUntilRespawn;
    }

    void Update()
    {
        if (!hasDied)
            timerToSavePos -= Time.deltaTime;
        if (hasDied)
        {
            timerUntilRespawn -= Time.deltaTime;
            if (timerUntilRespawn <= 0)
            {
                statuesObjective.Clear();
                player = GameObject.FindGameObjectWithTag("Player");
                playerUI = FindObjectOfType<PlayerUIController>();
                AddStatuesToList();
                Respawn();
                timerUntilRespawn = initRespawn;
            }
        }
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if (timerToSavePos <= 0)
        {
            lastKnownPlayerPosition = player.transform.position;
            lastKnownPlayerRot = player.transform.rotation;
            timerToSavePos = initTimer;
        }
    }

    void AddStatuesToList()
    {
        List<StatueObjectiveController> statuesTemporary;
        statuesTemporary = new List<StatueObjectiveController>(FindObjectsOfType<StatueObjectiveController>());
        foreach (StatueObjectiveController statues in statuesTemporary)
        {
            if(statues.isNotSecondary)
            {
                statuesObjective.Add(statues);
            }
        }
    }
    public void Respawn()
    {
        player.transform.position = lastKnownPlayerPosition;
        player.transform.rotation = lastKnownPlayerRot;
        objectivesCompleted = 0;
        for (int i = 0; i <= statuesObjective.Count - 1; i++)
        {
            if (objectivesIndex[i])
            {
                statuesObjective[i].gameObject.SetActive(false);
                Debug.Log(statuesObjective[i].gameObject.name);
                objectivesCompleted += 1;
            }
        }
        playerUI.objectivesCleared = objectivesCompleted;
        //playerUI.FillObjective();
        hasDied = false;       
    }

    public void OnStatueObjectiveDestroyed(StatueObjectiveController statue)
    {
        if (statuesObjective.Contains(statue))
        {
            objectivesIndex[statue.indexForGameController] = true;
            statuesObjective.Remove(statue);
            objectivesCompleted += 1;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadMode)
    {
        Debug.Log(objectivesIndex.Length);
        for(int i = 0; i <= objectivesIndex.Length - 1; i++)
        {
            objectivesIndex[i] = false;
        }
        statuesObjective.Clear();
        player = GameObject.FindGameObjectWithTag("Player");
        playerUI = FindObjectOfType<PlayerUIController>();
        AddStatuesToList();       
        timerUntilRespawn = initRespawn;
        objectivesCompleted = 0;      
        playerUI.objectivesCleared = objectivesCompleted;
        //playerUI.FillObjective();
        hasDied = false;
        Debug.Log(scene.name);
        Debug.Log(loadMode);
    }
}
