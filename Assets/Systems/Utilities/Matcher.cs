
using System;
using System.Collections;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using UnityEngine;
using Random = UnityEngine.Random;

public class Matcher : MonoBehaviour
{
    [SerializeField] private MatchSettings matchSettings;
    [SerializeField] private MatchUI matchUi;
    [SerializeField] private Transform[] teamSpawnPoints;
    [SerializeField] private CharactersRoster charactersRoster;
    
    private int team1PlayersCount;
    private int team2PlayersCount;

    
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        SpawnLocalPlayer();
    }
    
    private void OnEnable()
    {
        MatchEventsInvoker.OnPlayerIsDead += UpdateMatchScore;
        PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
    }

    private void OnDisable()
    {
        MatchEventsInvoker.OnPlayerIsDead -= UpdateMatchScore;
        PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
    }
    
    //todo: cleanup, event subscription on enable/disable
    private void Start()
    {
        InitializeMatchData();
    }

    private void InitializeMatchData()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            var players = PhotonNetwork.PlayerList;
            foreach (var player in players)
            {
                var teamNumber = (byte)player.CustomProperties[PhotonTeamsManager.TeamPlayerProp];
                if (teamNumber == 1)
                    team1PlayersCount++;
                else
                    team2PlayersCount++;
                
            }
            
            byte evCode = 5;
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            SendOptions sendOptions = new SendOptions { Reliability = true };
            PhotonNetwork.RaiseEvent(evCode, new []{matchSettings.Team1Score,matchSettings.Team2Score}, raiseEventOptions, sendOptions);
        }
    }

    private void SpawnLocalPlayer()
    {
        int spawnIndex = PhotonNetwork.LocalPlayer.GetPhotonTeam().Code-1;
        Vector3 spawnPoint = teamSpawnPoints[spawnIndex].position + Random.insideUnitSphere * 5;
        spawnPoint.y = 0;

        PhotonNetwork.Instantiate(charactersRoster.GetCharactersToSpawn().CharacterPlayablePrefab.name,
            spawnPoint, Quaternion.identity);
    }
    

    private void OnEvent(EventData photonEvent)
    {
        if(photonEvent.Code==2)
            UpdateMatchScore((byte)photonEvent.CustomData);
        else if(photonEvent.Code==3)
        {
            RoundTransition(()=>PhotonNetwork.LoadLevel(1));
        }
        else if (photonEvent.Code == 4)
        {
            int[] score = photonEvent.CustomData as int[];
            int winTeam = score[0]>score[1]?1:2;
            matchUi.UpdateScore(score[0], score[1]);
            
            RoundTransition(()=> matchUi.ShowMatchWinPanel(winTeam));
        }
        else if (photonEvent.Code==5)
        {
            int[] score = photonEvent.CustomData as int[];
            matchUi.UpdateScore(score[0],score[1]);
        }
    }


    private void UpdateMatchScore(PhotonView photonView)
    {
        var team = photonView.Controller.GetPhotonTeam().Code;
        
        if (!PhotonNetwork.IsMasterClient)
        {
            byte evCode = 2;
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.MasterClient };
            SendOptions sendOptions = new SendOptions { Reliability = true };
            PhotonNetwork.RaiseEvent(evCode, team, raiseEventOptions, sendOptions);
        }
        else
        {
            UpdateMatchScore(team);
        }
    }

    private void UpdateMatchScore(int team)
    {
        if (team == 1)
        {
            team1PlayersCount--;
            if (team1PlayersCount == 0)
            {
                matchSettings.Team2Score++;
                NextRound();
            }
        }
        else
        {
            team2PlayersCount--;
            if (team2PlayersCount == 0)
            {
                matchSettings.Team1Score++;
                NextRound();
            }
        }
    }

    private void NextRound()
    {
        matchSettings.CurrentRound++;

        if (matchSettings.MatchIsOver())
        {
            byte evCode = 4;
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            SendOptions sendOptions = new SendOptions { Reliability = true };
            PhotonNetwork.RaiseEvent(evCode, new []{matchSettings.Team1Score,matchSettings.Team2Score}, raiseEventOptions, sendOptions);
            
            matchSettings.ClearData();
        }
        else
        {
            byte evCode = 3;
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            SendOptions sendOptions = new SendOptions { Reliability = true };
            PhotonNetwork.RaiseEvent(evCode, null, raiseEventOptions, sendOptions);
        }
    }

    private void RoundTransition(Action onTransitionEndAction)
    {
        StartCoroutine(RoundTransitionCoroutine(onTransitionEndAction));
    }

    private IEnumerator RoundTransitionCoroutine(Action onTransitionEndAction)
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(1);

        Time.timeScale = 1;
        yield return new WaitForSeconds(1);
        onTransitionEndAction?.Invoke();
    }
}
