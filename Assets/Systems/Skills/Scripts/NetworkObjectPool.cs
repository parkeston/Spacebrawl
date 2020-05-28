using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Object = UnityEngine.Object;

public class NetworkObjectPool
{
    private readonly NetworkGameObject[] pool;
    private GameObject parentPoolObject;

    public NetworkObjectPool(int poolSize, GameObject prefabToPool, Transform arsenal, Action<NetworkGameObject> OnCreated = null)
    {
        parentPoolObject = new GameObject(prefabToPool.name+"Pool");
        parentPoolObject.transform.SetParent(arsenal);
        
        pool = new NetworkGameObject[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            pool[i] = PhotonNetwork.Instantiate(prefabToPool.name,Vector3.zero, Quaternion.identity).GetComponent<NetworkGameObject>();
            pool[i].SetParent(parentPoolObject.transform,Vector3.zero);
            pool[i].Activate(false,parentPoolObject.transform.position,Quaternion.identity);
            //pool[i].gameObject.SetActive(false);
            OnCreated?.Invoke(pool[i]);
        }
    }

    public NetworkGameObject GetPoolObject()
    {
        foreach (var poolObject in pool)
        {
            if (poolObject.gameObject.activeSelf) continue;
            //poolObject.gameObject.SetActive(true);
            return poolObject;
        }

        return null;
    }
}
