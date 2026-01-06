using System;
using UnityEngine;

/// <summary>
/// Manages game state and events
/// </summary>
public sealed class GameManager : MonoBehaviour
{
    // This can be used to check if the game is over from other scripts
    
    private void OnEnable()
    {
        GameEvents.GameOverRequested += GameOver;
    }

    private void OnDisable()
    {
        GameEvents.GameOverRequested -= GameOver;
    }

    public void GameOver()
    {
        GameEvents.RaiseGameOverChanged(true);
    }
}
