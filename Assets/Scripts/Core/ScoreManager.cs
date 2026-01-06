using System;
using UnityEngine;

public sealed class ScoreManager : MonoBehaviour
{
    [SerializeField] private AsteroidTypeDatabase asteroidConfigs;
    [SerializeField] private ScoringConfig scoring;

    public int Score { get; private set; }
    
    public int HighScore 
    { 
        get => PlayerPrefs.GetInt("HighScore", 0);
        set => PlayerPrefs.SetInt("HighScore", value); 
    }

    public void ResetScore()
    {
        Score = 0;
        GameEvents.RaiseScoreChanged(Score);
    }

    public void AddFor(AsteroidSize size)
    {
        int points = scoring != null ? scoring.GetPoints(size) : 0;
        Score += points;
        GameEvents.RaiseScoreChanged(Score);
    }

    public void RecordHighScore()
    {
        if(Score > HighScore) HighScore = Score;
    }
}