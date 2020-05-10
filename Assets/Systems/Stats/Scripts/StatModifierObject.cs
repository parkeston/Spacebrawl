using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class StatModifierObject : MonoBehaviour
{
    [SerializeField] private StatModifierTrigger trigger;
    [SerializeField] private StatModifierEffect statModifierEffect;
    [SerializeField] private bool destroyOnModify;

    private Collider collider;

    private void Awake()
    {
        collider = GetComponent<Collider>();
        collider.isTrigger = true; //if player is character controller then only trigger events are caught (can use 2 separate colliders for trigger & physics)
    }

    private void OnTriggerEnter(Collider other)
    {
        DelegateToModifier(StatModifierTrigger.TriggerEnter,other.GetComponents<IStat>());
    }

    private void OnTriggerStay(Collider other)
    {
        DelegateToModifier(StatModifierTrigger.TriggerStay,other.GetComponents<IStat>());

    }

    private void OnTriggerExit(Collider other)
    {
        DelegateToModifier(StatModifierTrigger.TriggerExit,other.GetComponents<IStat>());
    }

    private void DelegateToModifier(StatModifierTrigger trigger, IStat[] stats)
    {
        if(stats==null)
            return;
        
        if(trigger!=this.trigger)
            return;
        
        statModifierEffect.ApplyEffectTo(stats);
        if(destroyOnModify)
            Destroy(gameObject);
    }
}
