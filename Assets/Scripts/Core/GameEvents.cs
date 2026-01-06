using UnityEngine;

using System;

public static class GameEvents
{
    public static event Action<int> ScoreChanged;
    public static event Action<int> LivesChanged;
    public static event Action<int> WaveChanged;

    public static event Action<bool> GameOverChanged;
    
    public static event Action<Vector3> ExplosionRequested;
    public static void RaiseScoreChanged(int score) => ScoreChanged?.Invoke(score);
    public static void RaiseLivesChanged(int lives) => LivesChanged?.Invoke(lives);
    public static void RaiseWaveChanged(int wave) => WaveChanged?.Invoke(wave);
    public static void RaiseGameOverChanged(bool isGameOver) => GameOverChanged?.Invoke(isGameOver);
    public static void RaiseExplosionRequested(Vector3 pos) => ExplosionRequested?.Invoke(pos);
}

