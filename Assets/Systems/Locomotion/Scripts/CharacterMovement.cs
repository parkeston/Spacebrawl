using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
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

    private float velocityY;

    //todo: split to movable and rotatable? (so input too?)
    
    private void Awake()
    {
        controller = GetComponent<CharacterController>(); //no DI?
    }

    private void Update()
    {
        velocity = inputReader.GetMovementDirection() * movementSpeed;
        rotation = inputReader.GetRotation(transform.position);
        
        if (!controller.isGrounded)
        {
            velocityY -= 9.81f * Time.deltaTime;
            velocity += Vector3.up * velocityY;
        }
        else
            velocityY = 0;
    }

    private void FixedUpdate()
    {
        controller.Move(velocity * Time.fixedDeltaTime);
        transform.rotation =
            Quaternion.Lerp(transform.rotation, rotation, rotationLerpFactor);
    }
}