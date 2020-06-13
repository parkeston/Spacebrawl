
using System.Linq;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using UnityEngine;

public class QuickMatch : MonoBehaviourPunCallbacks
{
    [SerializeField] private byte maxPlayers = 4;

    private void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = maxPlayers;
        roomOptions.CustomRoomProperties = new Hashtable{{"mode","quick"}};
        roomOptions.CustomRoomPropertiesForLobby = new[] {"mode"};
        PhotonNetwork.CreateRoom(null, roomOptions);
    }

    public void SetPlayersCount(int count)
    {
        maxPlayers = (byte)count;
    }

    public void SearchForQuickMatch()
    {
        var filter = new Hashtable{{"mode","quick"}};
        PhotonNetwork.JoinRandomRoom(filter,maxPlayers);
    }

    public void CancelSearch()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        CreateRoom();
    }

    public override void OnJoinedRoom()
    {
        // joined a room successfully
    }

    private void LoadArena(int arena)
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
        }
        
        Debug.LogFormat("PhotonNetwork : Loading Level : {0}",arena);
        PhotonNetwork.LoadLevel(arena);
    }

    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting
        
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount==maxPlayers)
        {
            Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
            
            AssignTeams();
            PhotonNetwork.CurrentRoom.IsOpen = false;
            //LoadArena(1);
        }
    }
    
    private void AssignTeams()
    {
        //there are 2 teams in photon teams manager by default with code 1 & 2
        var players = PhotonNetwork.PlayerList;
        
        for (int i = 0; i < maxPlayers; i++)
        {
            int teamIndex = i / (maxPlayers / 2);
            players[i].JoinTeam((byte) (teamIndex + 1));
        }
    }

    private int updatedPlayersCount;
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            updatedPlayersCount++;
            if(updatedPlayersCount==maxPlayers)
                LoadArena(1);
        }
        print($"{targetPlayer.NickName} properties updated!");
    }
}
