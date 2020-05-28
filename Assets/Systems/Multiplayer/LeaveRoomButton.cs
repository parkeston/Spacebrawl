using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LeaveRoomButton : MonoBehaviourPunCallbacks
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(LeaveRoom);
    }

    /// <summary>
    /// Called when the local player left the room. We need to load the launcher scene.
    /// </summary>
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }
    
    private void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
    
}
