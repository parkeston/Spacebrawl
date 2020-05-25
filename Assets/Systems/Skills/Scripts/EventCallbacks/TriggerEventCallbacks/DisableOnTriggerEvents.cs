using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnTriggerEvents : TriggerEventCallback
{
    protected override void Callback(GameObject other)
    {
        gameObject.SetActive(false);
    }
}
