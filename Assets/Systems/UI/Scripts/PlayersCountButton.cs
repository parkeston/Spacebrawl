using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PlayersCountButton : MonoBehaviour
{
    [SerializeField] private Image backGround;
    [SerializeField] private TMP_Text text;

    [SerializeField] private Color textColor;
    [SerializeField] private Color textHighlightColor;

    private Button button;
    
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(()=>Select(true));
    }

    public void SetOnclickListener(UnityAction action)
    {
        button.onClick.AddListener(action);
    }
    
    public void Select(bool value)
    {
        text.color = value ? textHighlightColor : textColor;
        backGround.color = value ? Color.white : Color.clear;
    }
}
