using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeriodicModifierBehaviour : ModifierBehaviour
{
    [Header("Period settings")]
    [SerializeField] private float totalTime = 5;
    [SerializeField] private float periodTime = 1f;

    private Dictionary<IStat, Coroutine> modifierPerStat = new Dictionary<IStat, Coroutine>();
    
    protected override void ApplyEffectTo(IStat stat, float alterValue)
    {
        var statBehaviour = stat as Stat;
        if(statBehaviour==null)
            return;

        if (modifierPerStat.ContainsKey(stat))
        {
            if (modifierPerStat[stat] != null)
                statBehaviour.StopCoroutine(modifierPerStat[stat]);
            
            modifierPerStat[stat] = statBehaviour.StartCoroutine(ModifyStatsThroughTime(stat,alterValue));
        }
        else
        {
            modifierPerStat.Add(stat,statBehaviour.StartCoroutine(ModifyStatsThroughTime(stat,alterValue)));
        }
    }
    
    private IEnumerator ModifyStatsThroughTime(IStat stat, float alterValue)
    {
        float time = totalTime;
        
        while (time>0)
        {
            time -= periodTime;
            stat.ModifyStatValue(alterValue);
            
            yield return new WaitForSeconds(periodTime);
        }
    }
}
