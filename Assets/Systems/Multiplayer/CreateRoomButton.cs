using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CreateRoomButton : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField roomNameToCreate;
    [SerializeField] private UnityEvent OnRoomCreated;
    
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(CreateRoom);
    }

    private void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        
        if(!string.IsNullOrEmpty(roomNameToCreate.text))
            PhotonNetwork.CreateRoom(roomNameToCreate.text, roomOptions);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        print(message);
    }

    public override void OnCreatedRoom()
    {
        OnRoomCreated?.Invoke();
    }
}
