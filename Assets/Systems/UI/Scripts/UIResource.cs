using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIResource : MonoBehaviour
{
    public abstract void SetResourceDisplayColor(Color color);
    public abstract void UpdatePosition(Vector3 position);
    public abstract void UpdateResourceValue(float value, float maxValue);
}
