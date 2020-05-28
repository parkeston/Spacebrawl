using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LocomotionAnimator : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;

    private Animator locomotionAnimator;
    
    private float horizontalMotion;
    private float verticalMotion;
    
    private static readonly int HorizontalMotionParameter = Animator.StringToHash("horizontalMotion");
    private static readonly int VerticalMotionParameter = Animator.StringToHash("verticalMotion");

    private void Awake()
    {
        locomotionAnimator = GetComponent<Animator>(); //no DI?
    }

    private void Update()
    {
        Vector3 animationDirection = transform.InverseTransformDirection(inputReader.GetMovementDirection());
        horizontalMotion = animationDirection.x;
        verticalMotion = animationDirection.z;
    }

    private void FixedUpdate()
    {
        locomotionAnimator.SetFloat(HorizontalMotionParameter,horizontalMotion,0.1f,Time.fixedDeltaTime);
        locomotionAnimator.SetFloat(VerticalMotionParameter,verticalMotion,0.1f,Time.fixedDeltaTime);
    }
}
