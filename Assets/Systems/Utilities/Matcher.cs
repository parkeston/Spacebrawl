using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matcher : MonoBehaviour
{
    [SerializeField] private CharactersRoster charactersRoster;

    private void Awake()
    {
        foreach (var character in charactersRoster.GetCharactersToSpawn())
        {
            Instantiate(character.CharacterPlayablePrefab);
        }
    }
}
