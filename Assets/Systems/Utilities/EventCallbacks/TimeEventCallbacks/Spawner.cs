using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private bool alignRotation;
    [SerializeField] private GameObject spawnPrefab;
    [SerializeField] private GameObject parentTo;

    private GameObject spawnedObject;

    //todo: use pool?
    private  void Awake()
    {
        spawnedObject = Instantiate(spawnPrefab);
        spawnedObject.transform.position = transform.position;
        spawnedObject.SetActive(false);
        if(parentTo!=null)
            spawnedObject.transform.SetParent(parentTo.transform);
        
        if(spawnedObject.TryGetComponent(out Collider collider))
            Physics.IgnoreCollision(collider,spawnedObject.GetComponent<Collider>());
    }
    
    public void Spawn()
    {
        spawnedObject.transform.position = transform.position;
        if(alignRotation)
            spawnedObject.transform.rotation = transform.rotation;
        spawnedObject.SetActive(true);
    }
}
