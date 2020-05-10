using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StatModifierEffect
{
    [SerializeField] private StatType statTypeToAffect;
    [SerializeField] private StatModifierCalculator statModifierCalculator;
    [SerializeField] private StatModifierBehaviour statModifierBehaviour;
    [SerializeField] private float statAlterAmount;
    
    public void ApplyEffectTo(IStat[] stats)
    {
        foreach (var stat in stats)
        {
            if (stat.StatType == statTypeToAffect)
            {
                var alterValue =
                    statModifierCalculator.CalculateAffectValue(statAlterAmount, stat.StatValue);
                statModifierBehaviour.ModifyStat(stat,alterValue);
                
                break; //only one unique stat type possible on a game object
            }
        }
    }
}
