using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class MatchEventsInvoker : MonoBehaviour
{
    public static event Action<PhotonView> OnPlayerIsDead;

    public void PlayerISDead(PhotonView photonView)
    {
        OnPlayerIsDead?.Invoke(photonView);
    }
}
