using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class GameObjectPool<T> where T: MonoBehaviour
{
    private readonly T[] pool;
    private GameObject parentPoolObject;

    public GameObjectPool(int poolSize, T prefabToPool, Transform arsenal, Action<T> OnCreated = null)
    {
        parentPoolObject = new GameObject(prefabToPool.name+"Pool");
        parentPoolObject.transform.SetParent(arsenal);
        
        pool = new T[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            pool[i] = Object.Instantiate(prefabToPool,parentPoolObject.transform);
            pool[i].gameObject.SetActive(false);
            OnCreated?.Invoke(pool[i]);
        }
    }

    public T GetPoolObject()
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
