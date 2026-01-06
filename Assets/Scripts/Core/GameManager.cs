using UnityEngine;

/// <summary>
/// Manages game state and events
/// </summary>
public sealed class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerHealth player;
    [SerializeField] private ScoreManager score;
    
    // This can be used to check if the game is over from other scripts
    public bool IsGameOver => _isGameOver;
    private bool _isGameOver;

    private void Start() => Restart();

    public void GameOver()
    {
        _isGameOver = true;
        score?.RecordHighScore();
        GameEvents.RaiseGameOverChanged(true);
    }

    public void Restart()
    {
        _isGameOver = false;
        GameEvents.RaiseGameOverChanged(false);
    }
}
