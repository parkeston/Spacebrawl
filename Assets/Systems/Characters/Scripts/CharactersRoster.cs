using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharactersRoster : ScriptableObject
{
    [SerializeField] private Character[] characters;
    private readonly List<Character> selectedCharacters = new List<Character>();

    public int Length => characters.Length;
    public Character this[int i] => characters[i];
    
    public void MarkCharacterAsSelected(Character character)
    {
        selectedCharacters.Add(character);
    }

    public Character[] GetCharactersToSpawn()
    {
        var characterToSpawn = selectedCharacters.ToArray();
        selectedCharacters.Clear();
        return characterToSpawn;
    }
}
