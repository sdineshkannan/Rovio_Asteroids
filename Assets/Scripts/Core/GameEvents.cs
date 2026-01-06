using UnityEngine;
using System;

/// <summary>
/// Global events for game logic
/// </summary>
public static class GameEvents
{
    public static event Action<int> ScoreChanged;
    public static void RaiseScoreChanged(int score) => ScoreChanged?.Invoke(score);
    
    public static event Action<int> LivesChanged;
    public static void RaiseLivesChanged(int lives) => LivesChanged?.Invoke(lives);
    
    public static event Action<int> WaveChangedStarted;
    public static void RaiseWaveChangeStart(int wave) => WaveChangedStarted?.Invoke(wave);
    
    public static event Action WaveChanged;
    public static void RaiseWaveChanged() => WaveChanged?.Invoke();
    
    public static event Action<bool> GameOverChanged;
    public static void RaiseGameOverChanged(bool isGameOver) => GameOverChanged?.Invoke(isGameOver);
    
    public static event Action<int, int> GameOverWithScore;
    public static void RaiseGameOverWithScore(int score, int highScore) => GameOverWithScore?.Invoke(score, highScore);
    
    public static event Action<Vector3> ExplosionRequested;
    public static void RaiseExplosionRequested(Vector3 pos) => ExplosionRequested?.Invoke(pos);
    
    public static event System.Action<PlayerState> PlayerStateChanged;
    public static void RaisePlayerStateChanged(PlayerState state) => PlayerStateChanged?.Invoke(state);
    
    public static event System.Action ShootRequested;
    public static void RaiseShootRequested() => ShootRequested?.Invoke();
    
    public static event Action<int> ScoreAwarded;
    public static void RaiseScoreAwarded(int value) => ScoreAwarded?.Invoke(value);

    public static event Action GameOverRequested;
    public static void RaiseGameOverRequested() => GameOverRequested?.Invoke();
}

