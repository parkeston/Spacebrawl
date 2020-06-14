using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class StartGameButton : MonoBehaviour
{
    private Button button;
    
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        button.interactable = false;

        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;
        
        byte evCode = 1; // Custom Event 1: Used as "MoveUnitsToTargetPosition" event
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well
        SendOptions sendOptions = new SendOptions { Reliability = true };
        PhotonNetwork.RaiseEvent(evCode, null, raiseEventOptions, sendOptions);
    }
}
