using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ResourceAffector
{
    [SerializeField] private ResourceType resourceTypeToAffect;
    [SerializeField] private ResourceAffectorProcessor resourceAffectorProcessor;
    [SerializeField] private float resourceAlterAmount;

    public void Affect(IResource[] resources)
    {
        foreach (var resource in resources)
        {
            if (resource.ResourceType == resourceTypeToAffect)
            {
                var alterValue =
                    resourceAffectorProcessor.CalculateAffectValue(resourceAlterAmount, resource.ResourceValue);
                resource.AlterResourceAmount(alterValue);
            }
        }
    }
}
