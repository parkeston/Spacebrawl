using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class ModifierBehaviour : MonoBehaviour
{
    [SerializeField] private ModifierTrigger trigger;
    
    [Header("Modifier effect")]
    [SerializeField] private StatType statToAffect;
    [SerializeField] private ModifierCalculator modifierCalculator;
    [SerializeField] private float statAlterAmount;
    [SerializeField] private bool disableOnModify;

    private Collider collider;

    private void Awake()
    {
        collider = GetComponent<Collider>();
        collider.isTrigger = true; //if player is character controller then only trigger events are caught (can use 2 separate colliders for trigger & physics)
    }

    private void OnTriggerEnter(Collider other)
    {
        Modify(other.gameObject, ModifierTrigger.TriggerEnter);
    }

    private void OnTriggerStay(Collider other)
    {
        Modify(other.gameObject, ModifierTrigger.TriggerStay);
    }

    private void OnTriggerExit(Collider other)
    {
        Modify(other.gameObject, ModifierTrigger.TriggerExit);
    }
    
    private void Modify(GameObject targetObject, ModifierTrigger trigger)
    {
        if(trigger!=this.trigger)
            return;

        var targetObjectStats = targetObject.GetComponents<IStat>();
        var statToModify = targetObjectStats?.FirstOrDefault((stat) => stat.StatType == statToAffect);
        if(statToModify == null)
            return;
        
        var alterValue =
            modifierCalculator.CalculateAffectValue(statAlterAmount, statToModify.StatValue);
        
        ApplyEffectTo(statToModify,alterValue);
        
        if(disableOnModify)
            gameObject.SetActive(false);
    }

    protected abstract void ApplyEffectTo(IStat stat, float alterValue);
}
