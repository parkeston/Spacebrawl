using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTimeEvent : TimeEventCallback
{
    [SerializeField] private UnityEvent onTime;
    protected override void Callback()
    {
        onTime?.Invoke();
    }
}
