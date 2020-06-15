using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharactersRoster : ScriptableObject
{
    [SerializeField] private Character[] characters;
    private Character selectedCharacter;

    public int Length => characters.Length;
    public Character this[int i] => characters[i];
    
    public void MarkCharacterAsSelected(Character character)
    {
        selectedCharacter = character;
    }

    public Character GetCharactersToSpawn()
    {
        return selectedCharacter;
    }
}
