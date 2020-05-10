using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ModifierCalculator : ScriptableObject
{
   [SerializeField] private ModifierType modifierType;

   public virtual float CalculateAffectValue(float alterAmount, float statValue)
   {
      switch (modifierType)
      {
         case ModifierType.SimpleAddition:
            return alterAmount;
         case ModifierType.RelativePercentValues:
            return statValue * (alterAmount / 100f);
         case ModifierType.AbsolutePercentValues:
            return statValue * ((100-alterAmount )/ 100f);
         default:
            return alterAmount;
      }
   }
   
   private enum ModifierType
   {
      SimpleAddition,
      RelativePercentValues,
      AbsolutePercentValues
   }
}
