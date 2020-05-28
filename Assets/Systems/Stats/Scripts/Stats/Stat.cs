using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Stat: MonoBehaviour, IStat, IConsumable
{
    [SerializeField] private StatType statType;
    [SerializeField] private float maxValue;
    [SerializeField] private float startingValue;
    
    public event Action<Stat , float, float> OnValueChanged;

    private float value;
    
    public StatType StatType => statType;
    public float StatValue => value;
    

    private void Start()
    {
        value = startingValue;
        OnValueChanged?.Invoke(this,value,maxValue);
        print($"Init {statType} of {name} ");

    }

    public void ModifyStatValue(float alterAmount)
    {
        value += alterAmount;
        if (value > maxValue)
            value = maxValue;
        
        OnValueChanged?.Invoke(this,value,maxValue);
        print($"Modified {statType} of {name}");

    }

    public void SyncWithLocalValue(float value)
    {
        this.value = value;
    }

    public bool Consume(float consumeAmount)
    {
        if (consumeAmount > value)
            return false;

        if (consumeAmount <= 0)
            return true;
        
        value -= consumeAmount;
        OnValueChanged?.Invoke(this,value,maxValue);
        print($"Consume {statType} of {name}");

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

