using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class HeroGridCell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image heroIcon;
    [SerializeField] private TMP_Text name;
    [SerializeField] private TMP_Text role;
    [SerializeField] private TMP_Text descritpion;
    [SerializeField] private GameObject descriptionBox;
    [SerializeField] private GameObject selectionOutline;

    private Button button;
    
    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public void Deselect()
    {
        selectionOutline.SetActive(false);
    }
    
    public void SetOnClickListener(UnityAction action)
    {
        button.onClick.AddListener(action);
    }

    public void SetCharacterData(Character character)
    {
        heroIcon.sprite = character.Icon;
        name.text = character.name;
        role.text = character.Role;
        descritpion.text = character.Description;
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
