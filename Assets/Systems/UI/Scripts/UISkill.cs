using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISkill : MonoBehaviour
{
    [SerializeField] private Image skillIcon;
    [SerializeField] private float cooldownTimerPeriod;
    [SerializeField] private TMP_Text cooldownText;
    
    public void StartCooldownTimer(float cooldownTime)
    {
        StartCoroutine(CooldownTimerRoutine(cooldownTime));
    }

    IEnumerator CooldownTimerRoutine(float time)
    {
        skillIcon.color = Color.grey;
        cooldownText.enabled = true;
        
        while (time>0)
        {
            cooldownText.text = time.ToString("F1");
            time -= cooldownTimerPeriod;
            yield return new WaitForSeconds(cooldownTimerPeriod);
        }
        
        skillIcon.color = Color.white;
        cooldownText.enabled = false;
    }
}
