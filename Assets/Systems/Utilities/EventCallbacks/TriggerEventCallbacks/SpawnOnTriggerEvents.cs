using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SpawnOnTriggerEvents : TriggerEventCallback
{
    [SerializeField] private bool parentToTrigger;
    [SerializeField] private NetworkGameObject spawnPrefab;

    private NetworkGameObject spawnedObject;

    //todo: use pool?
    protected override void Awake()
    {
        base.Awake();
        
        spawnedObject = PhotonNetwork.Instantiate(spawnPrefab.name,transform.position,Quaternion.identity).GetComponent<NetworkGameObject>();
        spawnedObject.Activate(false,transform.position,Quaternion.identity);
        if(spawnedObject.TryGetComponent(out collider))
            Physics.IgnoreCollision(collider,spawnedObject.GetComponent<Collider>());
    }
    
    protected override void Callback(GameObject other)
    {
        Vector3 position = transform.position;

        if (parentToTrigger)
        {
            position = other.transform.position;
            position.y = transform.position.y;
            spawnedObject.Activate(true,position,Quaternion.identity);
            spawnedObject.SetParent(other.transform,Vector3.zero);
        }
        else
            spawnedObject.Activate(true,position,Quaternion.identity);
    }
}
