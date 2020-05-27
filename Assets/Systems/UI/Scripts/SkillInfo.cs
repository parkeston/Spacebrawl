using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillInfo : MonoBehaviour, IAmUISkill
{
     [Header("Basic visuals")]
     [SerializeField] private Image skillIcon;

    [Header("Description visuals")]
    [SerializeField] private TMP_Text skillNameText;
    [SerializeField] private TMP_Text skillDescriptionText;
    [SerializeField] private TMP_Text skillTimingsText;


    public void SetIcon(Sprite icon, Color color)
    {
        skillIcon.sprite = icon;
        skillIcon.color = color;
    }

    public void FillDescription(string name, string description, string timings)
    {
        skillNameText.text = name;
        skillDescriptionText.text = description;
        skillTimingsText.text = timings;
    }
}
