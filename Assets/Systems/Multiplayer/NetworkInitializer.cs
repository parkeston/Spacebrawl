using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class NetworkInitializer : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1";
    private bool isConnecting;

    private NetworkGameModeButton currentGameMode;
    
    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        PhotonNetwork.NickName = Guid.NewGuid().ToString();
    }
    
    public void ConnectToServer(NetworkGameModeButton gameMode)
    {
        currentGameMode = gameMode;
        
        if (!PhotonNetwork.IsConnected)
        {
            isConnecting = PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
            
           currentGameMode.Connecting();
        }
        else
        {
           currentGameMode.ConnectingSuccess();
        }
    }

    public override void OnConnectedToMaster()
    {
        if (isConnecting)
        {
            Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");
            currentGameMode.ConnectingSuccess();
            isConnecting = false;
        }

        if (!PhotonNetwork.InLobby)
            PhotonNetwork.JoinLobby();
    }
    
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
        
        currentGameMode.ConnectingFailed();
    }
}
