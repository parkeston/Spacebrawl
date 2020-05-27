using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//todo: make an interface instead of abstract class?
public abstract class Skill : MonoBehaviour
{
   [Header("General settings")]
   [SerializeField] protected float castTime;
   [SerializeField] protected float cooldownTime;
   [SerializeField] private float energyCost;

   public Transform ArsenalPoint { get; set; }
   public Collider OwnerCollider { get; set; }

   public float EnergyCost => energyCost;
   public bool IsCastCompleted { get; protected set; } = true;
   public abstract void Use(Transform origin, Stat energyStat);
   public abstract event Action<float> OnCoolDown;
   
   public abstract string GetDescriptionIntro() ;
   public virtual string GetTimingsInfo() => $"Cast: {castTime}s\nCooldown: {cooldownTime}s";
}
