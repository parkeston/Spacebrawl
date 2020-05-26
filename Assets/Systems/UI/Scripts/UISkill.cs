using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISkill : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Basic visuals")]
    [SerializeField] private Image skillBG;
    [SerializeField] private float cooldownTimerPeriod;
    [SerializeField] private TMP_Text cooldownText;

    [Header("Description visuals")]
    [SerializeField] private GameObject descriptionBox;
    [SerializeField] private TMP_Text skillNameText;
    [SerializeField] private TMP_Text skillDescriptionText;
    [SerializeField] private TMP_Text skillTimingsText;

    [Header("Energy cost visuals")] 
    [SerializeField] private GameObject energyCostBG;
    [SerializeField] private Image energyCostFill;

    public void UpdateEnergyCostFill(float skillCost, float hasCost)
    {
        energyCostFill.fillAmount = hasCost / skillCost;
    }
    
    public void DisplayEnergyCost(bool value)
    {
        energyCostFill.gameObject.SetActive(value);
        energyCostBG.SetActive(value);
    }
    
    public void FillDescription(string name, string description, string timings)
    {
        skillNameText.text = name;
        skillDescriptionText.text = description;
        skillTimingsText.text = timings;
    }
    
    public void StartCooldownTimer(float cooldownTime)
    {
        StartCoroutine(CooldownTimerRoutine(cooldownTime));
    }

    IEnumerator CooldownTimerRoutine(float time)
    {
        skillBG.color = Color.grey;
        cooldownText.enabled = true;
        
        while (time>0)
        {
            cooldownText.text = time.ToString("F1");
            time -= cooldownTimerPeriod;
            yield return new WaitForSeconds(cooldownTimerPeriod);
        }
        
        skillBG.color = Color.white;
        cooldownText.enabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        descriptionBox.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionBox.SetActive(false);
    }
}
