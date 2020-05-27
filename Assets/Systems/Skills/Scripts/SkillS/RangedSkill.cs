using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedSkill : Skill
{
    [Header("Projectile launch settings")]
    [SerializeField] private float projectileFlightTime;
    [SerializeField] private float projectileIdleTime;
    [SerializeField] private float maxCastDistance;
    [SerializeField] private Projectile projectilePrefab;  //not only projectiles, what about melee?
    
    [Header("Casters")]
    [SerializeField] private Caster caster; //multiple caster helpers?
    [SerializeField] private Caster casterHelper; //multiple caster helpers?

    private float projectileLifetime;
    private float timeToCooldown;

    private GameObjectPool<Projectile> projectilePool;
    
    public override event Action<float> OnCoolDown;

    private void Start()
    {
        projectileLifetime = projectileFlightTime + projectileIdleTime;
        
        projectilePool = new GameObjectPool<Projectile>((int)((castTime+projectileLifetime)/cooldownTime)+1,projectilePrefab, ArsenalPoint,
            (projectile) =>
            {
                Physics.IgnoreCollision(OwnerCollider,projectile.GetComponent<Collider>()); //todo: fix hardcode
            });
    }
    
    public override void Use(Transform origin, Stat energyStat)
    {
        if (timeToCooldown < Time.time && energyStat.Consume(EnergyCost))
        {
            timeToCooldown = Time.time + cooldownTime+castTime;
            
            caster.gameObject.SetActive(true);
            casterHelper?.gameObject.SetActive(true);
            
            IsCastCompleted = false;
            StartCoroutine(UseRoutine(origin));
        }
    }


    private IEnumerator UseRoutine(Transform origin)
    {
        float time = castTime;

        while (time>0)
        {
            caster.DrawCastTrajectory(origin.position,maxCastDistance,projectileFlightTime); //passing not used parameters?
            casterHelper?.DrawCastTrajectory(origin.position, maxCastDistance, projectileFlightTime);
            time -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        IsCastCompleted = true;
        OnCoolDown?.Invoke(cooldownTime);

        var projectile = projectilePool.GetPoolObject();
        projectile.transform.position = origin.position;
        projectile.gameObject.SetActive(true);//???
        caster.Cast(origin.position, projectile,projectileFlightTime);
        
        caster.gameObject.SetActive(false);
        casterHelper?.gameObject.SetActive(false);

        projectile.StartCoroutine(DisableAfter(projectile,projectileLifetime));
    }

    private IEnumerator DisableAfter(Projectile projectile, float time)
    {
        yield return new WaitForSeconds(time);
        projectile.gameObject.SetActive(false);
    }

    public override string GetDescriptionIntro()
    {
        return $"Launches {projectilePrefab.name} which ";
    }
}
