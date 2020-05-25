using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSet : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Transform skillsMountPoint;
    [SerializeField] private Skill[] skillsPrefabs;

    private Skill[] skills;
    private Skill previousSkill;
    
    private void Awake()
    {
        skills = new Skill[skillsPrefabs.Length];
        for (int i = 0; i < skillsPrefabs.Length; i++)
        {
            skills[i] = Instantiate(skillsPrefabs[i],skillsMountPoint);
        }

        previousSkill = skills[0];
    }

    private void Update()
    {
        int skillNumber = inputReader.GetCombatInput();
        if (skillNumber > 0 && previousSkill.IsCastCompleted)
        {
            skills[skillNumber - 1].Use(skillsMountPoint);
            previousSkill = skills[skillNumber - 1];
        }
    }
}
