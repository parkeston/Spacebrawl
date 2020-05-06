using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[Serializable]
public class Stat: MonoBehaviour, IStat, IConsumable
{
    [SerializeField] private StatType statType;
    [SerializeField] private float maxValue;
    [SerializeField] private float startingValue;

    private float value;
    
    public StatType StatType => statType;
    public float StatValue => value;

    private void Awake()
    {
        value = startingValue;
    }

    private void OnValidate()
    {
        if (startingValue > maxValue)
            startingValue = maxValue;
    }

    public void ModifyStatValue(float alterAmount)
    {
        value += alterAmount;
        if (value > maxValue)
            value = maxValue;
    }

    public bool Consume(float consumeAmount)
    {
        if (consumeAmount > value)
            return false;

        value -= consumeAmount;
        return true;
    }
}

