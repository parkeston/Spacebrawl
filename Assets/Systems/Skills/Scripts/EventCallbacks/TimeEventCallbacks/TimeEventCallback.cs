using System;
using System.Collections;
using System.Collections.Generic;
using Packages.Rider.Editor.UnitTesting;
using UnityEngine;

public abstract class TimeEventCallback : MonoBehaviour
{
    [SerializeField] private float time=0.5f;

    private Coroutine callbackRoutine;
    
    private void OnEnable()
    {
        callbackRoutine = StartCoroutine(CallbackRoutine());
    }

    private void OnDisable()
    {
        if (callbackRoutine != null)
        {
            StopCoroutine(callbackRoutine);
            //Callback()?
        }
    }

    private IEnumerator CallbackRoutine()
    {
        yield return new WaitForSeconds(time);
        Callback();
    }

    protected abstract void Callback();
}
