using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class StatModifierObject : MonoBehaviour
{
    [SerializeField] private StatModifier modifier;
    [SerializeField] private bool destroyOnModify;

    private Collider collider;

    private void Awake()
    {
        collider = GetComponent<Collider>();
        collider.isTrigger = true; //if player is character controller then only trigger events are caught (can use 2 separate colliders for trigger & physics)
    }

    private void OnTriggerEnter(Collider other)
    {
        ModifyOnCollision(other.GetComponents<IStat>());
    }

    private void ModifyOnCollision(IStat[] stats)
    {
        if(stats==null || stats.Length==0)
            return;
        
        modifier.Modify(stats);
        if(destroyOnModify)
            Destroy(gameObject);
    }
}
