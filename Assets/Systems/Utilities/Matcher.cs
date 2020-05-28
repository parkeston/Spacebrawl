using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

public class Matcher : MonoBehaviour
{
    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private CharactersRoster charactersRoster;

    private void Awake()
    {
        
        Vector3 spawnPosition = Random.insideUnitSphere * 5;
        spawnPosition.y = 0;

        PhotonNetwork.Instantiate(charactersRoster.GetCharactersToSpawn()[0].CharacterPlayablePrefab.name, spawnPosition, Quaternion.identity);
    }
}
