using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;

    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationLerpFactor;

    private CharacterController controller;

    private Vector3 velocity;
    private Quaternion rotation;

    //todo: split to movable and rotatable? (so input too?)
    
    private void Awake()
    {
        controller = GetComponent<CharacterController>(); //no DI?
    }

    private void Update()
    {
        velocity = inputReader.GetMovementDirection() * movementSpeed;
        rotation = inputReader.GetRotation(transform.position);
    }

    private void FixedUpdate()
    {
        controller.Move(velocity * Time.fixedDeltaTime);
        transform.rotation =
            Quaternion.Lerp(transform.rotation, rotation, rotationLerpFactor);
    }
}