using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayersCountPanel : MonoBehaviour
{
    private PlayersCountButton selectedButton;
    
    private void Start()
    {
        var playersCountButtons = GetComponentsInChildren<PlayersCountButton>();
        foreach (var playersCountButton in playersCountButtons)
        {
            playersCountButton.SetOnclickListener(() =>
            {
                if (selectedButton != null && selectedButton!=playersCountButton)
                {
                    selectedButton.Select(false);
                    selectedButton = null;
                }

                selectedButton = playersCountButton;
            });
        }
        
        playersCountButtons[0].GetComponent<Button>().onClick?.Invoke();
    }
}
