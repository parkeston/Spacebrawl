using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class LocomotionAnimator : MonoBehaviour
{
    [SerializeField] private Animator locomotionAnimator;
    [SerializeField] private InputReader inputReader;


    //how to enforce specifying animator with the same interface(set of params, states, transitions & blend trees)

    private float horizontalMotion;
    private float verticalMotion;
    
    private static readonly int HorizontalMotion = Animator.StringToHash("horizontalMotion");
    private static readonly int VerticalMotion = Animator.StringToHash("verticalMotion");

    private void Update()
    {
        //converting world space movement direction to local space direction, for properly setting animation
        Vector3 animationDirection = transform.InverseTransformDirection(inputReader.GetMovementDirection());
        horizontalMotion = animationDirection.x;
        verticalMotion = animationDirection.z;
    }

    private void FixedUpdate()
    {
        locomotionAnimator.SetFloat(HorizontalMotion,horizontalMotion);
        locomotionAnimator.SetFloat(VerticalMotion,verticalMotion);
    }
}
