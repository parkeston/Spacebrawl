using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class JoinButon : MonoBehaviourPunCallbacks
{
    [SerializeField] private RoomListPanel roomListPanel;
    [SerializeField] private UnityEvent onJoinedRoom;

    
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(JoinRoom);
    }

    private void JoinRoom()
    {
        if(roomListPanel.SelectedRoom!=null)
            PhotonNetwork.JoinRoom(roomListPanel.SelectedRoom.Name);
    }

    public override void OnJoinedRoom()
    { 
        onJoinedRoom?.Invoke();
    }
}
