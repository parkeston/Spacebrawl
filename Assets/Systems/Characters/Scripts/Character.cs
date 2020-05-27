using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu]
public class Character : ScriptableObject
{
    [SerializeField] private Sprite icon;
    [SerializeField] private string role;
    [TextArea] [SerializeField] private string description;
    [TextArea] [SerializeField] private string bio;

    [SerializeField] private GameObject characterPlayablePrefab;
    [SerializeField] private GameObject characterPreviewPrefab;

    public Sprite Icon => icon;
    public string Name => name;
    public string Role => role;
    public string Description => description;
    public string Bio => bio;
    public GameObject CharacterPlayablePrefab => characterPlayablePrefab;
    public GameObject CharacterPreviewPrefab => characterPreviewPrefab;
}
