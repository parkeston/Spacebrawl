using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ArcCaster : Caster
{
    [SerializeField] private int arcResolution;
    [SerializeField] private float gravity; //change to angle???

    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = arcResolution + 1;
    }

    protected override void CalculateTrajectory(Vector3 startPoint, RaycastHit target,float castDistance,  float projectileTime)
    {
        Vector3 velocity = CalculateVelocty(target.point, startPoint, projectileTime*0.8f,castDistance);
        lineRenderer.SetPositions(CalculateArcPositions(startPoint, velocity, projectileTime*0.8f));
    }

    public override void Cast(Vector3 startPoint, Projectile projectile, float projectileTime)
    {
        Vector3[] positions = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(positions);
        
        MoveProjectileAlongCurve(projectile,positions,0, projectileTime*0.8f);
        //projectile.Move(startPoint, lineRenderer.GetPosition(lineRenderer.positionCount - 1), projectileTime); //temp
    }

    private void MoveProjectileAlongCurve(Projectile projectile, Vector3[] positions, int index, float projectileTime)
    {
        if (index >= positions.Length-1)
            return;

        projectile.Move(positions[index], positions[index + 1], projectileTime / positions.Length,
            () => MoveProjectileAlongCurve(projectile, positions, index+1,projectileTime));
    }

    Vector3 CalculateVelocty(Vector3 target, Vector3 origin, float time, float castDistance)
    {
        Vector3 displacement = target - origin;
        Vector3 displacementXZ = displacement;
        displacementXZ.y = 0f;
        displacementXZ = Vector3.ClampMagnitude(displacementXZ, castDistance);

        float sY = displacement.y;
        float sXz = displacementXZ.magnitude;

        float Vxz = sXz / time;
        float Vy = (sY / time) - (0.5f * gravity * time);

        Vector3 result = displacementXZ.normalized;
        result *= Vxz;
        result.y = Vy;

        return result;
    }

    private Vector3[] CalculateArcPositions(Vector3 startPoint, Vector3 initialVelocity, float time)
    {
        Vector3[] arcPoints = new Vector3[arcResolution + 1];
        for (int i = 0; i <= arcResolution; i++)
        {
            float t = i / (float) arcResolution;
            float simulationTime = t * time;
            Vector3 displacement = initialVelocity * simulationTime +
                                   Vector3.up * (gravity * simulationTime * simulationTime) / 2f;
            Vector3 drawPoint = startPoint + displacement;
            arcPoints[i] = drawPoint;
        }

        return arcPoints;
    }
}