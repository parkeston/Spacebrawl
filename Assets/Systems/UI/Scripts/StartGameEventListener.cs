using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;

public class StartGameEventListener : MonoBehaviour
{
   [SerializeField] private UnityEvent onGameStarted;
   private void OnEnable()
   {
      PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
   }

   private void OnDisable()
   {
      PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
   }

   private void OnEvent(EventData photonEvent)
   {
      if(photonEvent.Code!=1)
         return;
      
      onGameStarted?.Invoke();
   }
}
