using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HeroPickScreen : MonoBehaviour
{
    [SerializeField] private CharactersRoster currentRoster;
    [SerializeField] private GameObject heroPickGrid;
    [SerializeField] private HeroInfo heroInfoPanel;
    [SerializeField] private HeroGridCell gridCellPrefab;
    [SerializeField] private Button playButton;

    private HeroGridCell selectedCell;
    private Character selectedCharacter;
    private GameObject previewedHero;
    
    private void Awake()
    {
        playButton.onClick.AddListener(PlayButtonPressed);
        FillHeroGrid();
    }

    private void FillHeroGrid()
    {
        for (int i = 0; i < currentRoster.Length; i++)
        {
            var heroGridCell = Instantiate(gridCellPrefab, heroPickGrid.transform, false);
            heroGridCell.SetCharacterData(currentRoster[i]);

            var i1 = i;
            heroGridCell.SetOnClickListener(() =>
            {
                if (selectedCell != null)
                {
                    Destroy(previewedHero);
                    selectedCell.Deselect();
                    selectedCharacter = null;
                }

                selectedCharacter = currentRoster[i1];
                previewedHero = Instantiate(currentRoster[i1].CharacterPreviewPrefab);
                selectedCell = heroGridCell;
                
                heroInfoPanel.DisplayHeroData(currentRoster[i1]);
                playButton.gameObject.SetActive(true);
            });
        }
    }

    private void PlayButtonPressed()
    {
        currentRoster.MarkCharacterAsSelected(selectedCharacter);
        selectedCharacter = null;
    }
}