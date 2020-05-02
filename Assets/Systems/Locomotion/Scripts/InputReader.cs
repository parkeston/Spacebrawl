using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputReader : ScriptableObject
{
    public abstract Vector3 GetMovementDirection();
    public abstract Quaternion GetRotation(Vector3 rotatingPoint);
}
