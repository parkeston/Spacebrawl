using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TimingModifier : StatModifierBehaviour
{
    //todo: make editable at stat modifier effect level in inspector ?
    [SerializeField] private float timeToModify = 5;
    [SerializeField] private float rateToModify = 1f;
    
    public override void ModifyStat(IStat stat, float alterValue)
    {
        var statBehaviour = stat as Stat; //todo: fix need of conversion
        if(statBehaviour==null)
            return;
        
        //check for several coroutines on one object?
        statBehaviour.StartCoroutine(ModifyStatThroughTime(stat, alterValue));
    }

    private IEnumerator ModifyStatThroughTime(IStat stat, float alterValue)
    {
        float time = timeToModify;
        
        while (time>0)
        {
            time -= rateToModify;
            stat.ModifyStatValue(alterValue);
            
            Debug.Log(time);
            yield return new WaitForSeconds(rateToModify);
        }
    }
}
