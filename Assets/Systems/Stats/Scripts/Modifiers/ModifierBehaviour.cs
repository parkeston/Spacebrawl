using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class ModifierBehaviour: MonoBehaviour
{
    [SerializeField] private ModifierTrigger trigger;

    [Header("Modifier effect")]
    [SerializeField] private StatType statToAffect;
    [SerializeField] private ModifierCalculator modifierCalculator;
    [SerializeField] private float statAlterAmount;
    [SerializeField] private float restTime;
    [SerializeField] private bool disableOnModify;

    //if player is character controller then only trigger events are caught (can use 2 separate colliders for trigger & physics)

    private float timeToRest;
    private Collider collider;

    private List<GameObject> targetObjects;
    
    private void Awake()
    {
        targetObjects = new List<GameObject>();
        collider = GetComponent<Collider>();
        collider.isTrigger = true;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (trigger == ModifierTrigger.TriggerEnter)
        {
            Modify(other.gameObject);
        }
        
        if(trigger==ModifierTrigger.TriggerStay)
            targetObjects.Add(other.gameObject);
        
        Debug.Log("Count: "+targetObjects.Count);
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (trigger == ModifierTrigger.TriggerStay && timeToRest < Time.time)
        {
            timeToRest = Time.time + restTime;
            foreach (var targetObject in targetObjects)
            {
                Modify(targetObject);
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (trigger == ModifierTrigger.TriggerExit) 
            Modify(other.gameObject);

        if (trigger == ModifierTrigger.TriggerStay)
            targetObjects.Remove(other.gameObject);
        
        Debug.Log("Exit: "+targetObjects.Count);
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

#if UNITY_EDITOR
    [ContextMenu("Copy Description")]
    private void CopyDescription()
    {
        EditorGUIUtility.systemCopyBuffer = GenerateModifierDescription();
    }
#endif
}
