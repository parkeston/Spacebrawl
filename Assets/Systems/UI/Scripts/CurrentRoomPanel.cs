using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class CurrentRoomPanel : MonoBehaviourPunCallbacks
{
   [SerializeField] private PlayerListItem playerListItemPrefab;
   [SerializeField] private Transform playerListTeam1;
   [SerializeField] private Transform playerListTeam2;
   [SerializeField] private SwitchTeamButton team1Button;
   [SerializeField] private SwitchTeamButton team2Button;
   [SerializeField] private TMP_Text roomName;
   
   [Header("Room settings")]
   [SerializeField] private GameObject settingsPanel;
   [SerializeField] private TMP_Dropdown maxPlayersCount;
   [SerializeField] private Button startGameButton;

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
      
      team1Button.OnSwitchingTeam += SwitchTeam;
      team2Button.OnSwitchingTeam += SwitchTeam;
      
      team1Button.Interactable = false;
      team2Button.Interactable = true;
      
      PhotonTeamsManager.PlayerJoinedTeam += OnJoinedTeam;
      PhotonTeamsManager.PlayerLeftTeam += OnLeftTeam;
      
      UpdateRoomInfo(PhotonNetwork.CurrentRoom);
      
      settingsPanel.SetActive(PhotonNetwork.IsMasterClient);
      if(PhotonNetwork.IsMasterClient)
         FillSettingsInfo(PhotonNetwork.CurrentRoom);
   }

   public override void OnDisable()
   {
      base.OnDisable();
      
      team1Button.OnSwitchingTeam -= SwitchTeam;
      team2Button.OnSwitchingTeam -= SwitchTeam;
      
      PhotonTeamsManager.PlayerJoinedTeam -= OnJoinedTeam;
      PhotonTeamsManager.PlayerLeftTeam -= OnLeftTeam;
      
      foreach (var existingPlayer in existingPlayers)
      {
         Destroy(existingPlayer.Value.gameObject);
      }
      existingPlayers.Clear();
      
      print("clearing player list on disable!");
   }
   
   private void SwitchTeam(int team)
   {
      PhotonNetwork.LocalPlayer.SwitchTeam((byte)team);
      
      team1Button.Interactable = team!=1;
      team2Button.Interactable = team!=2;
   }

   private void UpdateRoomInfo(Room roomInfo)
   {
      roomName.text = roomInfo.Name;
      
      var players = PhotonNetwork.PlayerList;
      foreach (var currentRoomPlayer in players)
      {
         AddPlayerToList(currentRoomPlayer);
      }
   }

   private void FillSettingsInfo(RoomInfo roomInfo)
   {
      startGameButton.interactable = false;
      
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
      var playerListItem = Instantiate(playerListItemPrefab, playerListTeam1, false);
      playerListItem.SetInfo(player);
      
      existingPlayers.Add(player,playerListItem);
      print("player added player to list!");
      
      AssignTeam(player,playerListItem);
   }
   

   public override void OnPlayerEnteredRoom(Player newPlayer)
   {
      AddPlayerToList(newPlayer);
   }

   public override void OnPlayerLeftRoom(Player otherPlayer)
   {
      Destroy(existingPlayers[otherPlayer].gameObject);
      existingPlayers.Remove(otherPlayer);
      
      print("removing player from list on room left!");
   }

   private void AssignTeam(Player player, PlayerListItem playerListItem)
   {
      int teamCode = player.GetPhotonTeam()?.Code ?? -1;
      if (PhotonNetwork.IsMasterClient && teamCode<0)
      {
         player.JoinTeam(1);
      }
      if(teamCode>0)
         playerListItem.transform.SetParent(teamCode>1?playerListTeam2:playerListTeam1,false);
   }
   

   private int[] playersCountPerTeam = new int[2];
   private void OnJoinedTeam(Player player, PhotonTeam photonTeam)
   {
      int team = photonTeam.Code;
      
      print($" player joined to team {team}");
      existingPlayers[player].transform.SetParent(team > 1 ? playerListTeam2 : playerListTeam1, false);
      
      if (PhotonNetwork.IsMasterClient)
      {
         playersCountPerTeam[team - 1]++;

         if (playersCountPerTeam[0] != 0 && playersCountPerTeam[1] != 0)
            startGameButton.interactable = true;
         else
            startGameButton.interactable = false;
      }
      
   }

   private void OnLeftTeam(Player player, PhotonTeam photonTeam)
   {
      if (PhotonNetwork.IsMasterClient)
      {
         playersCountPerTeam[photonTeam.Code - 1]--;

         if (playersCountPerTeam[0] != 0 && playersCountPerTeam[1] != 0)
            startGameButton.interactable = true;
         else
            startGameButton.interactable = false;
      }
   }
}
