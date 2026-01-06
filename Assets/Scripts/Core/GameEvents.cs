using UnityEngine;
using System;

/// <summary>
/// Global events for game logic
/// </summary>
public static class GameEvents
{
    public static event Action<int> ScoreChanged;
    public static event Action<int> LivesChanged;
    public static event Action<int> WaveChanged;

    public static event Action<bool> GameOverChanged;
    
    public static event Action<Vector3> ExplosionRequested;
    public static event System.Action<PlayerState> PlayerStateChanged;

    public static void RaiseScoreChanged(int score) => ScoreChanged?.Invoke(score);
    public static void RaiseLivesChanged(int lives) => LivesChanged?.Invoke(lives);
    public static void RaiseWaveChanged(int wave) => WaveChanged?.Invoke(wave);
    public static void RaiseGameOverChanged(bool isGameOver) => GameOverChanged?.Invoke(isGameOver);
    public static void RaiseExplosionRequested(Vector3 pos) => ExplosionRequested?.Invoke(pos);
    public static void RaisePlayerStateChanged(PlayerState state) => PlayerStateChanged?.Invoke(state);
}

