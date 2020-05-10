using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatModifierBehaviour : ScriptableObject
{
    public abstract void ModifyStat(IStat stat, float alterValue);
}
