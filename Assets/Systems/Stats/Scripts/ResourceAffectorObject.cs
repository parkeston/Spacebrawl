using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ResourceAffectorObject : MonoBehaviour
{
    [SerializeField] private ResourceAffector[] affectors;
    [SerializeField] private bool isTrigger;
    [SerializeField] private bool destroyOnAffect;

    private Collider collider;

    private void Awake()
    {
        collider = GetComponent<Collider>();
        collider.isTrigger = isTrigger;
    }

    private void OnCollisionEnter(Collision other)
    {
        AffectOnCollision(other.gameObject.GetComponents<IResource>());
    }

    private void OnTriggerEnter(Collider other)
    {
        AffectOnCollision(other.GetComponents<IResource>());
    }

    private void AffectOnCollision(IResource[] resources)
    {
        if(resources==null || resources.Length==0)
            return;
        
        foreach (var affector in affectors)
        {
            affector.Affect(resources);
        }
        
        if(destroyOnAffect)
            Destroy(gameObject);
    }
}
