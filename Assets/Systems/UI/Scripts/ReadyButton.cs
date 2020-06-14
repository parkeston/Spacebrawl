using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ReadyButton : MonoBehaviour
{
    [SerializeField] private bool isReady;

    private Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ChangePlayerReadyState);
    }

    private void ChangePlayerReadyState()
    {
        PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable {{"_rd", isReady}});
    }
}
