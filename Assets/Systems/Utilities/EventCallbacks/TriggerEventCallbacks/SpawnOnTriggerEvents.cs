using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class SpawnOnTriggerEvents : TriggerEventCallback
{
    [SerializeField] private bool parentToTrigger;
    [SerializeField] private GameObject spawnPrefab;

    private GameObject spawnedObject;

    //todo: use pool?
    protected override void Awake()
    {
        base.Awake();
        
        spawnedObject = Instantiate(spawnPrefab);
        spawnedObject.transform.position = transform.position;
        spawnedObject.SetActive(false);
        if(spawnedObject.TryGetComponent(out collider))
            Physics.IgnoreCollision(collider,spawnedObject.GetComponent<Collider>());
    }
    
    protected override void Callback(GameObject other)
    {
        spawnedObject.transform.position = transform.position;

        if (parentToTrigger)
        {
            Vector3 position = other.transform.position;
            position.y = transform.position.y;
            spawnedObject.transform.position = position;
            spawnedObject.transform.SetParent(other.transform);
        }
        spawnedObject.SetActive(true);
    }
}
