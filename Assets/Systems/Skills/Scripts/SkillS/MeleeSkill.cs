using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MeleeSkill : Skill
{
   [Header("Melee settings")]
   [SerializeField] private float prepareTime;
   [SerializeField] private int maxComboAttacks;
   [SerializeField] private GameObject weaponPoint;
   [SerializeField] private Melee meleePrefab;
   [SerializeField] private NetworkGameObject vfxObjectPrefab;

   private Animator animator;
   private Melee melee;
   private float timeToCooldown;
   private int attackCounter;
   
   private NetworkGameObject vfxObject;

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

      vfxObject = PhotonNetwork.Instantiate(vfxObjectPrefab.name, Vector3.zero, Quaternion.identity)
         .GetComponent<NetworkGameObject>();
      
      vfxObject.SetParent(transform,Vector3.zero);
      vfxObject.Activate(false,transform.position,Quaternion.identity);
      
      melee = PhotonNetwork.Instantiate(meleePrefab.name,Vector3.zero, Quaternion.identity).GetComponent<Melee>();
      melee.transform.SetParent(ArsenalPoint);
      melee.Target = weaponPoint.transform;
      
      Physics.IgnoreCollision(OwnerCollider,melee.Collider);
   }

   public void OnAttackStarted()
   {
      melee.Collider.enabled = true;
      melee.Sync(true);
      vfxObject.Activate(true,transform.position,transform.rotation);
   }

   public void OnAttackEnded()
   {
      melee.Collider.enabled = false;
      melee.Sync(false);
      vfxObject.Activate(false,transform.position,transform.rotation);
   }
   
   public override void Use(Transform origin, Stat energyStat)
   {
      if (timeToCooldown < Time.time && energyStat.Consume(EnergyCost))
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
