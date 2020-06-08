using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrentRoomPanel : MonoBehaviourPunCallbacks
{
   [SerializeField] private PlayerListItem playerListItemPrefab;
   [SerializeField] private Transform playerList;
   [SerializeField] private TMP_Text roomName;
   
   [Header("Room settings")]
   [SerializeField] private GameObject settingsPanel;
   [SerializeField] private TMP_Dropdown maxPlayersCount;

   private Dictionary<Player,PlayerListItem> existingPlayers = new Dictionary<Player, PlayerListItem>();
   private int playersCountDropDownValue;
   
   private void Awake()
   {
      maxPlayersCount.onValueChanged.AddListener(ChangeRoomMaxPlayers);
   }

   private void ChangeRoomMaxPlayers(int value)
   {
      int playersCount = int.Parse(maxPlayersCount.options[value].text);
      if (PhotonNetwork.CurrentRoom.PlayerCount > playersCount)
      {
         maxPlayersCount.value = playersCountDropDownValue;
      }
      else
      {
         PhotonNetwork.CurrentRoom.MaxPlayers = (byte) playersCount;
         playersCountDropDownValue = value;
      }
   }

   public override void OnEnable()
   { 
      base.OnEnable();
      UpdateRoomInfo(PhotonNetwork.CurrentRoom);
      
      settingsPanel.SetActive(PhotonNetwork.IsMasterClient);
      if(PhotonNetwork.IsMasterClient)
         FillSettingsInfo(PhotonNetwork.CurrentRoom);
   }

   public override void OnDisable()
   {
      base.OnDisable();
      
      foreach (var existingPlayer in existingPlayers)
      {
         Destroy(existingPlayer.Value.gameObject);
      }
      existingPlayers.Clear();
   }

   private void UpdateRoomInfo(Room roomInfo)
   {
      roomName.text = roomInfo.Name;
      
      foreach (var currentRoomPlayer in roomInfo.Players)
      {
         AddPlayerToList(currentRoomPlayer.Value);
      }
   }

   private void FillSettingsInfo(RoomInfo roomInfo)
   {
      for (int i=0;i<maxPlayersCount.options.Count;i++)
      {
         if (maxPlayersCount.options[i].text == roomInfo.MaxPlayers.ToString())
         {
            maxPlayersCount.value = i;
            playersCountDropDownValue = i;
            break;
         }
      }
   }

   private void AddPlayerToList(Player player)
   {
      var playerListItem = Instantiate(playerListItemPrefab, playerList, false);
      playerListItem.SetInfo(player);
      
      existingPlayers.Add(player,playerListItem);
   }

   public override void OnPlayerEnteredRoom(Player newPlayer)
   {
      AddPlayerToList(newPlayer);
   }

   public override void OnPlayerLeftRoom(Player otherPlayer)
   {
      Destroy(existingPlayers[otherPlayer].gameObject);
      existingPlayers.Remove(otherPlayer);
   }
}
