using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ResourceAffectorProcessor : ScriptableObject
{
   [SerializeField] private ProcessingType processingType;

   public virtual float CalculateAffectValue(float alterAmount, float resourceValue)
   {
      switch (processingType)
      {
         case ProcessingType.SimpleAddition:
            return alterAmount;
         case ProcessingType.RelativePercentValues:
            return resourceValue * (alterAmount / 100f);
         case ProcessingType.AbsolutePercentValues:
            return resourceValue * ((100-alterAmount )/ 100f);
         default:
            return alterAmount;
      }
   }
   
   private enum ProcessingType
   {
      SimpleAddition,
      RelativePercentValues,
      AbsolutePercentValues
   }
}
