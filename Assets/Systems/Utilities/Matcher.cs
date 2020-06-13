using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine;
using Random = UnityEngine.Random;

public class Matcher : MonoBehaviour
{
    [SerializeField] private Transform[] teamSpawnPoints;
    [SerializeField] private CharactersRoster charactersRoster;

    private void Awake()
    {
        int spawnIndex = PhotonNetwork.LocalPlayer.GetPhotonTeam().Code-1;
        Vector3 spawnPosition = teamSpawnPoints[spawnIndex].position + Random.insideUnitSphere * 5;
        spawnPosition.y = 0;

        PhotonNetwork.Instantiate(charactersRoster.GetCharactersToSpawn()[0].CharacterPlayablePrefab.name,
            spawnPosition, Quaternion.identity);
    }
}
