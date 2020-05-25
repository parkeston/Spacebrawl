using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnTime : TimeEventCallback
{
    protected override void Callback()
    {
        gameObject.SetActive(false);
    }
}
