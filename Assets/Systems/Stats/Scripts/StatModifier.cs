using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StatModifier
{
    [SerializeField] private StatType statTypeToAffect;
    [SerializeField] private StatModifierBehaviour statModifierBehaviour;
    [SerializeField] private float statAlterAmount;

    public void Modify(IStat[] stats)
    {
        foreach (var stat in stats)
        {
            if (stat.StatType == statTypeToAffect)
            {
                var alterValue =
                    statModifierBehaviour.CalculateAffectValue(statAlterAmount, stat.StatValue);
                stat.ModifyStatValue(alterValue);
            }
        }
    }
}
