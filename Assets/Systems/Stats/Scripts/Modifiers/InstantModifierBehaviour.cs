using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantModifierBehaviour : ModifierBehaviour
{
    protected override void ApplyEffectTo(IStat stat, float alterValue)
    {
       stat.ModifyStatValue(alterValue);
    }
}
