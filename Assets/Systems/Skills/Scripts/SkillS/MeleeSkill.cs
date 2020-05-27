﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MeleeSkill : Skill
{
   [Header("Melee settings")]
   [SerializeField] private float prepareTime;
   [SerializeField] private int maxComboAttacks;
   [SerializeField] private GameObject weaponPoint;
   [SerializeField] private Melee meleePrefab;

   private Animator animator;
   private Melee melee;
   private float timeToCooldown;
   private int attackCounter;

   private static readonly int AttackCounter = Animator.StringToHash("attackCounter");
   private static readonly int Attack = Animator.StringToHash("attack");
   
   public override event Action<float> OnCoolDown;

   private void Reset()
   {
      if(weaponPoint==null)
         weaponPoint = new GameObject("WeaponPoint");
      weaponPoint.transform.SetParent(transform);
      weaponPoint.transform.localPosition = Vector3.zero;
   }

   private void Start()
   {
      animator = GetComponent<Animator>();
      
      melee = Instantiate(meleePrefab,ArsenalPoint);
      melee.Target = weaponPoint.transform;
      melee.Collider.enabled = false;
      
      Physics.IgnoreCollision(OwnerCollider,melee.Collider);
   }

   public void OnAttackStarted()
   {
      melee.Collider.enabled = true;
      melee.Sync(true);

   }

   public void OnAttackEnded()
   {
      melee.Collider.enabled = false;
      melee.Sync(false);
   }
   
   public override void Use(Transform origin)
   {
      if (timeToCooldown < Time.time)
      {
         timeToCooldown = Time.time + cooldownTime+prepareTime;
         
         IsCastCompleted = false;
         StartCoroutine(UseRoutine(origin));
      }
   }


   private IEnumerator UseRoutine(Transform origin)
   {
      yield return new WaitForSeconds(prepareTime); 
      
      OnCoolDown?.Invoke(cooldownTime);

      if (attackCounter >= maxComboAttacks)
         attackCounter = 0;
      
      attackCounter++;
      animator.SetTrigger(Attack);
      animator.SetInteger(AttackCounter,attackCounter);
      
      yield return  new WaitForSeconds(castTime);
      IsCastCompleted = true;
   }

   public override string GetTimingsInfo() => $"Cast: {prepareTime}s\nCooldown: {cooldownTime}s";

   public override string GetDescriptionIntro()
   {
      return $"Melee attack which ";
   }
}
