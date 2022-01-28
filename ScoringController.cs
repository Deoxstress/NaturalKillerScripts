using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoringController : MonoBehaviour
{
    [SerializeField] private Text textTime;
    [SerializeField] private float time;
    [SerializeField] private string timeFormat;
    [SerializeField] private Text timesHit;
    private PlayerMovement pm;
    void Awake()
    {
        pm = FindObjectOfType<PlayerMovement>();
    }   
    void Update()
    {
        if(!pm.LockAllInputs)
            time += Time.deltaTime;
        textTime.text = FormatTime(time);
        timesHit.text = "" + pm.timesHit;
    }

    private string FormatTime(float time)
    {
        int intTime = (int)time;
        int minutes = intTime / 60;
        int seconds = intTime % 60;
        float fraction = time * 1000;
        fraction = (fraction % 1000);
        timeFormat = String.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, fraction);       
        return timeFormat;
    }
}
