using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TimeEventCallback : MonoBehaviour
{
    [SerializeField] private bool startCountdownAtObjectActivation = true;
    [SerializeField] private float time=0.5f;

    private Coroutine callbackRoutine;
    
    private void OnEnable()
    {
        if(startCountdownAtObjectActivation)
           StarCountDown();
    }

    private void OnDisable()
    {
        if (callbackRoutine != null)
            StopCoroutine(callbackRoutine);
    }

    public void StarCountDown()
    {
        if(callbackRoutine!=null)
            StopCoroutine(callbackRoutine);
        
        callbackRoutine = StartCoroutine(CallbackRoutine());
    }

    private IEnumerator CallbackRoutine()
    {
        yield return new WaitForSeconds(time);
        Callback();
    }

    protected abstract void Callback();
}
