using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Caster : MonoBehaviour
{
    [SerializeField] protected CasterSharedSettings settings;

    public void DrawCastTrajectory(Vector3 startPoint, float castDistance, float projectileTime)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, settings.MaxRayDistance, settings.CastLayer))
        {
           CalculateTrajectory(startPoint,hitInfo, castDistance,projectileTime);
        }
    }
    
    protected abstract void CalculateTrajectory(Vector3 startPoint, RaycastHit target, float castDistance, float projectileTime);
    public abstract void Cast(Vector3 startPoint, Projectile projectile, float projectileTime);
}
