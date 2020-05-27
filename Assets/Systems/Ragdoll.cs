using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    private Rigidbody[] rigidbodies;
    private Collider[] colliders;

    private void Awake()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();

        for (int i = 0; i < rigidbodies.Length; i++)
        {
            rigidbodies[i].isKinematic = true;
            colliders[i].enabled = false;
        }
    }

    public void ActivateRagdoll(bool value)
    {
        if (value)
        {
            for (int i = 0; i < rigidbodies.Length; i++)
            {
                colliders[i].enabled = true;
                rigidbodies[i].isKinematic = false;
            }
        }
    }
}
