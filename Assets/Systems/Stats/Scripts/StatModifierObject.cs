using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class StatModifierObject : MonoBehaviour
{
    [SerializeField] private StatModifier[] modifiers;
    [SerializeField] private bool isTrigger;
    [SerializeField] private bool destroyOnModify;

    private Collider collider;

    private void Awake()
    {
        collider = GetComponent<Collider>();
        collider.isTrigger = isTrigger;
    }

    private void OnCollisionEnter(Collision other)
    {
        ModifyOnCollision(other.gameObject.GetComponents<IStat>());
    }

    private void OnTriggerEnter(Collider other)
    {
        ModifyOnCollision(other.GetComponents<IStat>());
    }

    private void ModifyOnCollision(IStat[] stats)
    {
        if(stats==null || stats.Length==0)
            return;
        
        foreach (var modifier in modifiers)
        {
            modifier.Modify(stats);
        }
        
        if(destroyOnModify)
            Destroy(gameObject);
    }
}
