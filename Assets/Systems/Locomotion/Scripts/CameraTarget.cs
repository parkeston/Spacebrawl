using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    private Vector3 offset;
    private new Camera camera;

    private void Awake()
    {
        camera = Camera.main;
        offset = camera.transform.position - transform.position;
    }

    private void LateUpdate()
    {
        camera.transform.position = transform.position + offset;
    }
}
