using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AreaCaster : Caster
{
    [SerializeField] private Sprite areaSprite;
    [SerializeField] private float radius; //todo: max automatically setup

    private SpriteRenderer spellArea;
    
    private void Awake()
    {
        spellArea = GetComponent<SpriteRenderer>();
        spellArea.sprite = areaSprite;

        transform.localScale = Vector3.one * radius;
    }
    
    protected override void CalculateTrajectory(Vector3 startPoint, RaycastHit target, float castDistance, float projectileTime)
    {
        Vector3 castDirection = target.point - startPoint;
        castDirection.y = 0;
        castDirection = Vector3.ClampMagnitude(castDirection, castDistance );

        startPoint.y = (target.point + settings.VisualsGroundOffset).y;
        transform.position = startPoint + castDirection;
        transform.rotation = Quaternion.LookRotation(target.normal);  //todo: optimize, set rotation once?
    }

    public override void Cast(Vector3 startPoint, Projectile projectile, float projectileTime)
    {
        projectile.Teleport(transform.position-settings.VisualsGroundOffset,projectileTime);
    }
}
