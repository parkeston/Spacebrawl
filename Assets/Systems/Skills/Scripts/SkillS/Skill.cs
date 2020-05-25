using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//todo: make an interface instead of abstract class?
public abstract class Skill : MonoBehaviour
{
   [SerializeField] private string skillName;
   [TextArea] [SerializeField] private string description;

   [SerializeField] protected float castTime;
   [SerializeField] protected float cooldownTime;

   public bool IsCastCompleted { get; protected set; } = true;
   public abstract void Use(Transform origin);
   public abstract event Action<float> OnCoolDown;
}
