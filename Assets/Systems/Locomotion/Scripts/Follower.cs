using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private Transform follow;

    private Vector3 offset;

    private void Awake()
    {
        offset = transform.position-follow.position;
    }

    private void LateUpdate()
    {
        transform.position = follow.position + offset;
    }
}
