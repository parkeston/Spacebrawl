using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class PlayerListItem : MonoBehaviour
{
    [SerializeField] private TMP_Text playerName;

    public void SetInfo(Player player)
    {
        playerName.text = player.NickName;
    }
    
}
