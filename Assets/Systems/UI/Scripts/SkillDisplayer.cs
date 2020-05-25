using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Skill))]
public class SkillDisplayer : MonoBehaviour
{
    [SerializeField] private UISkill uiSkillPrefab;
    [SerializeField] private SkillUICanvas canvasPrefab;

    private static SkillUICanvas canvas;
    private UISkill uiSkill;

    private Skill skill;
    
    private void Awake()
    {
        if (canvas == null)
            canvas = Instantiate(canvasPrefab);

        uiSkill = Instantiate(uiSkillPrefab, canvas.SkillPanel.transform, false);
        skill = GetComponent<Skill>();
        skill.OnCoolDown += uiSkill.StartCooldownTimer;
    }
}
