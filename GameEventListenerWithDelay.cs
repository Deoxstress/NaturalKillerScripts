using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListenerWithDelay : GameEventListener
{
    [SerializeField] private float delay = 1.0f;
    [SerializeField] private UnityEvent delayedUnityEvent;

    private void Awake()
    {
        gameEvent.Register(this);
    }

    private void OnDestroy()
    {
        gameEvent.Deregister(this);
    }

    public override void RaiseEvent()
    {
        unityEvent.Invoke();
        StartCoroutine(RunDelayedEvent());
    }

    private IEnumerator RunDelayedEvent()
    {
        yield return new WaitForSeconds(delay);
        delayedUnityEvent.Invoke();
    }
}
