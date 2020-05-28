using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

public class Matcher : MonoBehaviour
{
    [SerializeField] private GameObject characterPrefab;

    private void Awake()
    {
        Vector3 spawnPosition = Random.insideUnitSphere * 5;
        spawnPosition.y = 0;

        PhotonNetwork.Instantiate(characterPrefab.name, spawnPosition, Quaternion.identity);
    }
}
