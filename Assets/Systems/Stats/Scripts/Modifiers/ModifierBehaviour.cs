using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class ModifierBehaviour : TriggerEventCallback
{
    [Header("Modifier effect")]
    [SerializeField] private StatType statToAffect;
    [SerializeField] private ModifierCalculator modifierCalculator;
    [SerializeField] private float statAlterAmount;
    [SerializeField] private bool disableOnModify;

    //if player is character controller then only trigger events are caught (can use 2 separate colliders for trigger & physics)

    protected override void Callback(GameObject other)
    {
        Modify(other.gameObject);
    }

    private void Modify(GameObject targetObject)
    {
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

    private  string GenerateModifierDescription()
    {
        StringBuilder stringBuilder = new StringBuilder(100);
        stringBuilder.Append(statAlterAmount > 0 ? "increases target's " : "decreases target's ");
        stringBuilder.Append(statToAffect.name);
        stringBuilder.Append(" by ");
        stringBuilder.Append(GetEffectDescription(Mathf.Abs(statAlterAmount),modifierCalculator.GetModificationType()));

        return stringBuilder.ToString();
    }

    protected abstract string GetEffectDescription(float statAffectValue, string modifcationType);

    [ContextMenu("Copy Description")]
    private void CopyDescription()
    {
        EditorGUIUtility.systemCopyBuffer = GenerateModifierDescription();
    }
}
