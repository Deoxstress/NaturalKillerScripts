using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveController : MonoBehaviour
{

    [SerializeField] private Text[] objectiveText; //Text Component from gameobjects.
    [SerializeField] private List<string> objectiveList; //describe each objectives.
    [SerializeField] private bool objectiveComplete;
    [SerializeField] private bool objectiveLocked;// controls each objective status.
    [SerializeField] private List<Enemy> enemiesAnchored; // Enemies attached to each objectives.
    [Range(1, 3)]
    [SerializeField] private int difficultyTier; // 1 for easy , 2 for medium , 3 for hard;

    static ObjectiveController instance;
    public static ObjectiveController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<ObjectiveController>();
            }
            return instance;
        }
    }

    void Awake()
    {
        for (int counter = 0; counter <= objectiveText.Length - 1; counter++)
        {
            objectiveText[counter].text = objectiveList[counter];
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if(!objectiveComplete)
        {
            if(enemiesAnchored.Count == 0)
            {
                objectiveComplete = true;
            }
        }

        if (objectiveComplete && !objectiveLocked)
        {
            objectiveText[0].color = Color.green;
            objectiveLocked = true;
        }
    }

    public void OnObjectiveEnemyDestroyed(Enemy enemy)
    {
        if(enemiesAnchored.Contains(enemy))
        {
            enemiesAnchored.Remove(enemy);
        }
    }
}
