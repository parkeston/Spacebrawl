using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEvent : TriggerEventCallback
{
    [SerializeField] private UnityEvent onTrigger;

    protected override void Callback(GameObject other)
    {
       onTrigger.Invoke();
    }
}
