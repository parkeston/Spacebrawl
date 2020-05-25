using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class SpawnOnTriggerEvents : TriggerEventCallback
{
    [SerializeField] private GameObject spawnPrefab;

    private GameObject spawnedObject;

    //todo: use pool?
    protected override void Awake()
    {
        base.Awake();
        
        spawnedObject = Instantiate(spawnPrefab, transform.position, Quaternion.identity);
        spawnedObject.SetActive(false);
        if(spawnedObject.TryGetComponent(out collider))
            Physics.IgnoreCollision(collider,spawnedObject.GetComponent<Collider>());
    }
    
    protected override void Callback(GameObject other)
    {
        spawnedObject.transform.position = transform.position;
        spawnedObject.SetActive(true);
    }
}
