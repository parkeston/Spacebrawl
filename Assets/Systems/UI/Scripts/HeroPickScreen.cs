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
    [SerializeField] private Button cancelSearchButton;

    private HeroGridCell selectedCell;
    private Character selectedCharacter;
    private GameObject previewedHero;

    private bool isSearching;
    
    private void Start()
    {
        playButton.onClick.AddListener(PlayButtonPressed);
        cancelSearchButton.onClick.AddListener(CancelButtonPressed);
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
                if(isSearching)
                    return;
                
                if (selectedCell != null)
                {
                    Destroy(previewedHero);
                    selectedCell.Select(false);
                    selectedCharacter = null;
                }

                selectedCharacter = currentRoster[i1];
                previewedHero = Instantiate(currentRoster[i1].CharacterPreviewPrefab);
                selectedCell = heroGridCell;
                selectedCell.Select(true);
                
                heroInfoPanel.DisplayHeroData(currentRoster[i1]);
                playButton.gameObject.SetActive(true);
            });
        }
    }

    private void PlayButtonPressed()
    {
        currentRoster.MarkCharacterAsSelected(selectedCharacter);
        isSearching = true;
    }

    private void CancelButtonPressed()
    {
        isSearching = false;
    }

    public void DisableScreen()
    {
        if (previewedHero != null)
        {
            Destroy(previewedHero);
            selectedCell.Select(false);
            selectedCell = null;
            selectedCharacter = null;
            heroInfoPanel.HideHeroData();
            
            if(isSearching)
                cancelSearchButton.onClick.Invoke();
            
            playButton.gameObject.SetActive(false);
            cancelSearchButton.gameObject.SetActive(false);
        }
    }
}