using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InstantModifier : StatModifierBehaviour
{
    public override void ModifyStat(IStat stat, float alterValue)
    {
        stat.ModifyStatValue(alterValue);
    }
}
