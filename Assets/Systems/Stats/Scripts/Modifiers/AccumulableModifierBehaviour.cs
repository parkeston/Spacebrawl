using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class AccumulableModifierBehaviour : ModifierBehaviour
{
    [Header("Accumulation settings")]
    [SerializeField] private bool applyEffectWhenAccumulated;
    [SerializeField] private float accumulationSpeed;
    
    private Dictionary<IStat, float> accumulationFactorPerStat = new Dictionary<IStat, float>();
    
    protected override void ApplyEffectTo(IStat stat, float alterValue)
    {
        float currentFactor;
        if (!accumulationFactorPerStat.ContainsKey(stat))
        {
            accumulationFactorPerStat.Add(stat,accumulationSpeed);
            currentFactor = accumulationSpeed;
        }
        else
        {
            var factor = accumulationFactorPerStat[stat] + accumulationSpeed;
            currentFactor = factor;
            if (factor >= 1)
                factor = 0;
            accumulationFactorPerStat[stat] = factor;
        }
        
        if(!applyEffectWhenAccumulated)
            stat.ModifyStatValue(alterValue*currentFactor);
        else if (currentFactor>=1)
            stat.ModifyStatValue(alterValue);
    }

    protected override string GetEffectDescription(float statAffectValue, string modificationType)
    {
        if(applyEffectWhenAccumulated)
            return $"{statAffectValue}{modificationType} every {Mathf.RoundToInt(1/accumulationSpeed)} hit";

        StringBuilder stringBuilder = new StringBuilder(50);
        for (float factor = accumulationSpeed; factor <=1f; factor+=accumulationSpeed)
        {
            var effect = factor * statAffectValue;
            stringBuilder.Append($"{effect}/");
        }

        stringBuilder.Remove(stringBuilder.Length - 1, 1);
        stringBuilder.Append($"{modificationType} per hit");
        return stringBuilder.ToString();
    }
}
