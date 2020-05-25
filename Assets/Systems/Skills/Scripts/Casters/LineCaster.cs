using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class LineCaster : Caster
{
    [SerializeField] private Sprite lineSprite;

    private SpriteRenderer spriteRenderer;

    private Vector3 targetPoint;
    
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = lineSprite;
    }

    protected override void CalculateTrajectory(Vector3 startPoint, RaycastHit target, float castDistance, float projectileTime)
    {
        Vector3 castDirection = target.point - startPoint;
        castDirection.y = 0;

        startPoint.y = (target.point + settings.VisualsGroundOffset).y;
        targetPoint = startPoint + castDirection.normalized * castDistance; //todo: temp solution
        
        Vector3 scale = transform.localScale;
        scale.y = castDistance;
        transform.localScale = scale;
        
        transform.position = startPoint + castDirection.normalized * castDistance / 2;
        transform.rotation = Quaternion.LookRotation(target.normal,castDirection.normalized); //optimize?
    }

    public override void Cast(Vector3 startPoint, Projectile projectile, float projectileTime)
    {
        Vector3 endPoint = targetPoint;
        endPoint.y = startPoint.y;
        
        projectile.Move(startPoint,endPoint,projectileTime);
    }
}
