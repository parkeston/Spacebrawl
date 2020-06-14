using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PlayersReadyListener : MonoBehaviourPunCallbacks
{
    private int readyPlayers;

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            bool isReady = (bool)changedProps["_rd"];
            if (isReady)
                readyPlayers++;
            else
                readyPlayers--;
            
            if(readyPlayers==PhotonNetwork.CurrentRoom.PlayerCount)
                PhotonNetwork.LoadLevel(1);
        }
    }
}
