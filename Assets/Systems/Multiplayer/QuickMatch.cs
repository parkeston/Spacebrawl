
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class QuickMatch : MonoBehaviourPunCallbacks
{
    [SerializeField] private byte maxPlayers = 4;

    private void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = maxPlayers;
        PhotonNetwork.CreateRoom(null, roomOptions);
    }

    public void SetPlayersCount(int count)
    {
        maxPlayers = (byte)count;
    }

    public void SearchForQuickMatch()
    {
        PhotonNetwork.JoinRandomRoom(null, maxPlayers);
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
            LoadArena(1);
        }
    }
}
