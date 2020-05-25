using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStat
{
   StatType StatType { get; }
   float StatValue { get; }
   void ModifyStatValue (float alterAmount);
}
