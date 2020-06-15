using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MatchSettings : ScriptableObject
{
    [SerializeField] private int maxRoundAmount;
    [SerializeField] private int desiredScoreDifference;

    public int Team1Score { get; set; }
    public int Team2Score { get; set; }

    public int CurrentRound { get; set; } = 1;

    public bool MatchIsOver() =>
        CurrentRound > maxRoundAmount || Mathf.Abs(Team1Score - Team2Score) >= desiredScoreDifference;

    public void ClearData()
    {
        Team1Score = 0;
        Team2Score = 0;
        CurrentRound = 1;
    }

}
