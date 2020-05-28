using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StatMonitor : MonoBehaviour
{
    [SerializeField] private Stat statToMonitor;
    [SerializeField] private float targetStatValue;
    [SerializeField] private UnityEvent onTargetValueReached;

    private void Awake()
    {
        statToMonitor.OnValueChanged += MonitorValue;
    }

    private void MonitorValue(Stat modifiedStat, float value, float maxValue)
    {
        if(value<=targetStatValue)
            onTargetValueReached?.Invoke();
    }
}
