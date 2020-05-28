using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField] private Vector3 positionOffset;
    [SerializeField] private Vector3 rotation;
    
    private new Camera camera;

    private void Start()
    {
        camera = Camera.main;
        camera.transform.rotation = Quaternion.Euler(rotation);
    }

    private void LateUpdate()
    {
        camera.transform.position = transform.position + positionOffset;
    }
}
