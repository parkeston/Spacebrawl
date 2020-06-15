using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MatchUI : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text winText;
    [SerializeField] private GameObject winPanel;

    public void UpdateScore(int team1Score, int team2Score)
    {
        scoreText.text = $"{team1Score}/{team2Score}";
    }

    public void ShowMatchWinPanel(int team)
    {
        winText.text = $"TEAM {team} WON!";
        winPanel.SetActive(true);
    }
}
