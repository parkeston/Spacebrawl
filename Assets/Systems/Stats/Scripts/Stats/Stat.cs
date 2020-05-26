﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Stat: MonoBehaviour, IStat, IConsumable
{
    [SerializeField] private StatType statType;
    [SerializeField] private float maxValue;
    [SerializeField] private float startingValue;
    
    public event Action<float, float> OnValueChanged;

    private float value;
    
    public StatType StatType => statType;
    public float StatValue => value;

    private void Start()
    {
        value = startingValue;
        OnValueChanged?.Invoke(value,maxValue);
    }

    public void ModifyStatValue(float alterAmount)
    {
        value += alterAmount;
        if (value > maxValue)
            value = maxValue;
        
        OnValueChanged?.Invoke(value,maxValue);
    }
    
    public bool Consume(float consumeAmount)
    {
        if (consumeAmount > value)
            return false;

        value -= consumeAmount;
        OnValueChanged?.Invoke(value,maxValue);
        return true;
    }
    
    private void OnValidate()
    {
        if (startingValue > maxValue)
            startingValue = maxValue;

        if (GetComponents<IStat>().Any(stat => stat != this && stat.StatType == StatType))
        {
            Debug.LogError($"{name} cannot have more than one {StatType.name} stat type!");
            statType = null;
        }
    }
}

