using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Melee : MonoBehaviour
{
    [Header("Look direction settings")]
    [SerializeField] private Directions lookForwardVectorIsTargets;
    [SerializeField] private Directions lookUpVectorIsTargets;
    
    [Header("Target following settings")]
    [SerializeField] private int offsetDirection;
    [SerializeField] private float targetOffset;
    [SerializeField] private float targetLerpFactor;
    
    [Header("Tilting settings")]
    [SerializeField] private Vector3 tiltVector;
    [SerializeField] private float maxTiltAngle;
    [SerializeField] private float tiltLerpFactor;

    private Func<Vector3> getLookForwardVector;
    private Func<Vector3> getLookUpVector;
    
    private Quaternion startRotation;
    private Quaternion desiredRotation;

    private float currentTargetLerpFactor;
    private float currentTiltLerpFactor;

    private Rigidbody rigidbody;
    
    public Collider Collider { get; private set; }
    public Transform Target { get; set; }

    private void Awake()
    {
        Collider = GetComponent<Collider>();
        rigidbody = GetComponent<Rigidbody>();
        
        startRotation = rigidbody.rotation;

        currentTargetLerpFactor = targetLerpFactor;
        currentTiltLerpFactor = tiltLerpFactor;

        getLookForwardVector = SetUpLookVectors(lookForwardVectorIsTargets);
        getLookUpVector = SetUpLookVectors(lookUpVectorIsTargets);
    }
    
    private void FixedUpdate()
    {
        startRotation = Quaternion.Lerp(startRotation,Quaternion.LookRotation(getLookForwardVector(),getLookUpVector()),currentTargetLerpFactor );
        Vector3 targetPosition = Target.position + startRotation * Vector3.forward * (offsetDirection * targetOffset); //remember
        rigidbody.position = targetPosition;

        desiredRotation = startRotation* Quaternion.Euler(tiltVector*maxTiltAngle);
        rigidbody.rotation = Quaternion.Lerp(transform.rotation,desiredRotation,currentTiltLerpFactor);
    }

    public void Sync(bool value)
    {
        if (value)
        {
            currentTargetLerpFactor = 1;
            currentTiltLerpFactor = 1;
        }
        else
        {
            currentTargetLerpFactor = targetLerpFactor;
            currentTiltLerpFactor = tiltLerpFactor;
            
            print("synced off");
        }
    }

    private Func<Vector3> SetUpLookVectors(Directions directionsVector)
    {
        switch (directionsVector)
        {
            case Directions.Forward: return ()=> Target.forward;
            case Directions.Right: return () => Target.right;
            case Directions.Up: return () => Target.up;
            case Directions.ReverseForward: return () => -Target.forward;
            case Directions.ReverseRight: return () => -Target.right;
            case Directions.ReverseUp: return () => -Target.up;
            default:return ()=>Vector3.zero;
        }
    }

    private enum Directions
    {
        Forward,
        Up,
        Right,
        ReverseForward,
        ReverseUp,
        ReverseRight
    }
}
