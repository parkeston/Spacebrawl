﻿using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LeaveRoomButton : MonoBehaviourPunCallbacks
{
    [SerializeField] private UnityEvent onLeftRoom;
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(LeaveRoom);
    }
    
    public override void OnLeftRoom()
    {
        onLeftRoom?.Invoke();
        
        if(SceneManager.GetActiveScene().buildIndex!=0)
            SceneManager.LoadScene(0);
    }
    
    private void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
    
}
