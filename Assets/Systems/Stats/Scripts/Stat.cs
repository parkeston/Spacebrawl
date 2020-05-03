using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[Serializable]
public class Stat: MonoBehaviour, IResource, IConsumable
{
    [SerializeField] private ResourceType resourceType;
    [SerializeField] private float maxValue;
    [SerializeField] private float startingValue;

    private float value;
    
    public ResourceType ResourceType => resourceType;
    public float ResourceValue => value;

    private void Awake()
    {
        value = startingValue;
    }

    private void OnValidate()
    {
        if (startingValue > maxValue)
            startingValue = maxValue;
    }

    public void AlterResourceAmount(float alterAmount)
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

