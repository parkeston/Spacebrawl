using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SwitchTeamButton : MonoBehaviour
{
    [SerializeField] private int teamToSwitchTo;
    private Button button;

    public event Action<int> OnSwitchingTeam;

    public bool Interactable
    {
        set
        {
            if (button == null)
                button = GetComponent<Button>();
            button.interactable = value;
        }
    }
    
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(()=>OnSwitchingTeam?.Invoke(teamToSwitchTo));
    }
}
