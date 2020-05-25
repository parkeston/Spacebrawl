using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CasterSharedSettings : ScriptableObject
{
    [SerializeField] private float maxRayDistance;
    [SerializeField] private LayerMask castLayer;
    [SerializeField] private Vector3 visualsGroundOffset;

    public float MaxRayDistance => maxRayDistance;
    public LayerMask CastLayer => castLayer;
    public Vector3 VisualsGroundOffset => visualsGroundOffset;
}
