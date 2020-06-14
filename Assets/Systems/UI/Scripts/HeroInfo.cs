using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeroInfo : MonoBehaviour
{
    [Header("Name visuals")]
    [SerializeField] private TMP_Text name;
    [SerializeField] private GameObject nameLabel;
    
    [Header("General info visuals")]
    [SerializeField] private TMP_Text role;
    [SerializeField] private TMP_Text description;
    [SerializeField] private TMP_Text bio;
    
    [Header("Skills info visuals")]
    [SerializeField] private GameObject skillsInfoList;
    [SerializeField] private SkillInfo skillInfoPrefab;
    
    private int numberOfSkillsPerCharacter = 3;
    private SkillInfo[] skillInfos;
    
    private void Awake()
    {
        skillInfos = new SkillInfo[numberOfSkillsPerCharacter];
        
        for (int i = 0; i < numberOfSkillsPerCharacter; i++)
        {
            skillInfos[i] = Instantiate(skillInfoPrefab,skillsInfoList.transform);
        }
    }

    public void DisplayHeroData(Character character)
    {
        name.text = character.name;
        role.text = character.Role;
        description.text = character.Description;
        bio.text = character.Bio;

        var characterSkills = character.CharacterPlayablePrefab.GetComponentInChildren<SkillSet>().Skills;
        for(int i=0;i<characterSkills.Length;i++)
        {
            if(characterSkills[i].TryGetComponent(out SkillDisplayer skillDisplayer))
                skillDisplayer.FillUIElement(skillInfos[i]);
        }
        
        skillsInfoList.SetActive(true);
        nameLabel.SetActive(true);
    }

    public void HideHeroData()
    {
        skillsInfoList.SetActive(false);
        nameLabel.SetActive(false);
    }
}
