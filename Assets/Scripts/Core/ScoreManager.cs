using System;
using UnityEngine;

/// <summary>
/// Manages score and high score
/// </summary>
public sealed class ScoreManager : MonoBehaviour
{
    public int Score { get; private set; }

    public int HighScore 
    { 
        get => PlayerPrefs.GetInt("HighScore", 0);
        set => PlayerPrefs.SetInt("HighScore", value); 
    }

    private void OnEnable()
    {
        GameEvents.GameOverChanged += OnGameOverChanged;
    }

    private void OnDisable()
    {
        GameEvents.GameOverChanged -= OnGameOverChanged;
    }

    private void OnGameOverChanged(bool isGameOver)
    {
        if (isGameOver) RecordHighScore();
        else ResetScore();
    }
    
    public void ResetScore()
    {
        Score = 0;
        GameEvents.RaiseScoreChanged(Score);
    }

    public void Add(int value)
    {
        if (value <= 0) return;

        Score += value;
        GameEvents.RaiseScoreChanged(Score);
    }

    public void RecordHighScore()
    {
        if(Score > HighScore) HighScore = Score;
    }
}