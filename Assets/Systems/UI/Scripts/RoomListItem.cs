using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RoomListItem : MonoBehaviour
{
    [SerializeField] private TMP_Text roomName;
    [SerializeField] private TMP_Text roomPlayers;

    private Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public void SetOnClickListener(UnityAction action)
    {
        button.onClick.AddListener(action);
    }

    public void UpdateInfo(RoomInfo roomInfo)
    {
        roomName.text = roomInfo.Name;
        roomPlayers.text = $"{roomInfo.PlayerCount}/{roomInfo.MaxPlayers}";
    }
}
