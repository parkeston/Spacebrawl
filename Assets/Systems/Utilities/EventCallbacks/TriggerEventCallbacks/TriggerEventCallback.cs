using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class TriggerEventCallback : MonoBehaviour
{
    [SerializeField] private ModifierTrigger trigger;

    protected Collider collider;
    protected virtual void Awake()
    {
        collider = GetComponent<Collider>();
        collider.isTrigger = true;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(trigger==ModifierTrigger.TriggerEnter)
            Callback(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
       if(trigger==ModifierTrigger.TriggerExit)
           Callback(other.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if(trigger==ModifierTrigger.TriggerStay)
            Callback(other.gameObject);
    }

    protected abstract void Callback(GameObject other);
}
