using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIntroController : MonoBehaviour
{
    [SerializeField] private GameEvent BeholderIntroPart1, BeholderIntroPart2, BeholderFinishIntro, OnMoonIntro1;
    [SerializeField] private float timerUntilBeholderFinishIntro, timerUntilMoonBossFinishIntro;
    [SerializeField] private bool beholderPart1, beholderPart2, beholderPart3, beholderIntroFinish, moonPart1, moonPart2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timerUntilBeholderFinishIntro > 0)
        {
            timerUntilBeholderFinishIntro -= Time.deltaTime;
            if(timerUntilBeholderFinishIntro < 6f && beholderPart1)
            {
                beholderPart1 = false;
                BeholderIntroPart1.Invoke();
            }
            if (timerUntilBeholderFinishIntro < 4f && beholderPart2)
            {
                beholderPart2 = false;
                BeholderIntroPart2.Invoke();
            }            
            if(timerUntilBeholderFinishIntro < 2.5f && beholderIntroFinish)
            {
                beholderIntroFinish = false;
                BeholderFinishIntro.Invoke();
            }
        }
        if(timerUntilMoonBossFinishIntro > 0)
        {
            timerUntilMoonBossFinishIntro -= Time.deltaTime;
            if(timerUntilMoonBossFinishIntro < 2.9f && moonPart1)
            {
                moonPart1 = false;
                OnMoonIntro1.Invoke();
            }
        }
    }

    public void SetTimerIntroBeholder(float timerValue)
    {
        timerUntilBeholderFinishIntro = timerValue;
    }

    public void SetTimerMoonBossIntro(float timerValue)
    {
        timerUntilMoonBossFinishIntro = timerValue;
    }
}
