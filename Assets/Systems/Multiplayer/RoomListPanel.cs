using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class RoomListPanel : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform content;
    [SerializeField] private RoomListItem roomListItemPrefab;

    private readonly Dictionary<RoomInfo, RoomListItem> rooms = new Dictionary<RoomInfo, RoomListItem>();

    public RoomInfo SelectedRoom { get; private set; }

    public override void OnDisable()
    {
        base.OnDisable();
        foreach (var room in rooms)
        {
            Destroy(room.Value.gameObject);
        }
        rooms.Clear();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (var roomInfo in roomList.Where(roomInfo => (string) roomInfo.CustomProperties["mode"] != "quick"))
        {
            if (roomInfo.RemovedFromList)
            {
                if (rooms.ContainsKey(roomInfo))
                {
                    Destroy(rooms[roomInfo].gameObject);
                    rooms.Remove(roomInfo);
                }
            }
            else
            {
                if (rooms.ContainsKey(roomInfo))
                {
                    rooms[roomInfo].UpdateInfo(roomInfo);
                    return;
                }
                
                var roomButton = Instantiate(roomListItemPrefab, content, false);
                roomButton.UpdateInfo(roomInfo);
                rooms.Add(roomInfo,roomButton);
                
                roomButton.SetOnClickListener(() =>
                {
                    SelectedRoom = roomInfo;
                });
            }
        }
    }
}
