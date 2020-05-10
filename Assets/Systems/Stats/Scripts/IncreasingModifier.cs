using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class IncreasingModifier : StatModifierBehaviour
{
    [SerializeField] private bool applyEffectOnlyAtFactorCompletion;
    [SerializeField] private float factorSpeed;

    private Dictionary<IStat, float> increasingFactorPerStat = new Dictionary<IStat, float>();

    public override void ModifyStat(IStat stat, float alterValue)
    {
        float currentFactor;
        if (!increasingFactorPerStat.ContainsKey(stat))
        {
            increasingFactorPerStat.Add(stat,factorSpeed);
            currentFactor = factorSpeed;
        }
        else
        {
            var factor = increasingFactorPerStat[stat] + factorSpeed;
            currentFactor = factor;
            if (factor >= 1)
                factor = 0;
            increasingFactorPerStat[stat] = factor;
        }
        
        if(!applyEffectOnlyAtFactorCompletion)
            stat.ModifyStatValue(alterValue*currentFactor);
        else if (currentFactor>=1)
            stat.ModifyStatValue(alterValue);
    }
}
