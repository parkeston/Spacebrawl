using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private bool localRotation;
    [SerializeField] private Vector3 rotationAxis;
    [SerializeField] private float rotationSpeed;

    private void Update()
    {
        transform.Rotate(rotationAxis,rotationSpeed*Time.deltaTime,localRotation?Space.Self:Space.World);
    }
}
