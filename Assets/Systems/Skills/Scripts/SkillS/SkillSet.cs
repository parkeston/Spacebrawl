using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSet : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Stat energyStat;
    [SerializeField] private Skill[] skillsPrefabs;
    
    [Header("Positioning objects")]
    [SerializeField] private Transform skillsMountPoint;
    [SerializeField] private Collider bodyCollider;
    [SerializeField] private Transform arsenal;

    private Skill[] skills;
    private List<SkillDisplayer> skillDisplayers;
    private Skill previousSkill;
    
    private void Awake()
    {
        skills = new Skill[skillsPrefabs.Length];
        skillDisplayers = new List<SkillDisplayer>(skillsPrefabs.Length);
        
        for (int i = 0; i < skillsPrefabs.Length; i++)
        {
            skills[i] = Instantiate(skillsPrefabs[i], skillsMountPoint);
            skills[i].OwnerCollider = bodyCollider;
            skills[i].ArsenalPoint = arsenal;
            
            if(skills[i].TryGetComponent(out SkillDisplayer skillDisplayer))
                skillDisplayers.Add(skillDisplayer);
        }

        previousSkill = skills[0];

        energyStat.OnValueChanged += UpdateSkillDisplayers;
    }

    private void UpdateSkillDisplayers(float value, float maxValue)
    {
        foreach (var skillDisplayer in skillDisplayers)
        {
            skillDisplayer.UpdateEnergyCostVisuals(value);
        }
    }

    private void Update()
    {
        int skillNumber = inputReader.GetCombatInput();
        if (skillNumber > 0 && previousSkill.IsCastCompleted)
        {
            skills[skillNumber - 1].Use(skillsMountPoint, energyStat);
            previousSkill = skills[skillNumber - 1];
        }
    }
}