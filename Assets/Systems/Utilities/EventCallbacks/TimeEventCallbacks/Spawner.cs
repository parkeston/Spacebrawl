using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private bool alignRotation;
    [SerializeField] private NetworkGameObject spawnPrefab;
    [SerializeField] private GameObject parentTo;

    private NetworkGameObject spawnedObject;

    //todo: use pool?
    private  void Awake()
    {
        spawnedObject = PhotonNetwork.Instantiate(spawnPrefab.name,transform.position,spawnPrefab.transform.rotation).GetComponent<NetworkGameObject>();
        spawnedObject.Activate(false,transform.position,Quaternion.identity);
        if(parentTo!=null)
            spawnedObject.SetParent(parentTo.transform,Vector3.zero);
        
        if(spawnedObject.TryGetComponent(out Collider collider))
            Physics.IgnoreCollision(collider,spawnedObject.GetComponent<Collider>());
    }
    
    public void Spawn()
    {
        Quaternion rotation =spawnedObject.transform.rotation;
        if(alignRotation)
           rotation = transform.rotation;
        
        spawnedObject.Activate(true,transform.position,rotation);
        
        print("spawn");
    }
}
