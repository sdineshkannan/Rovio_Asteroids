using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerHealth player;
    [SerializeField] private ScoreManager score;
    [SerializeField] private WaveManager waves;
    [SerializeField] private UIController ui;

    // This can be used to check if the game is over from other scripts
    public bool IsGameOver => _isGameOver;
    private bool _isGameOver;

    private void Start() => Restart();

    public void GameOver()
    {
        _isGameOver = true;
        score?.RecordHighScore();
        if (ui != null) ui.SetGameOverVisible(true);
        GameEvents.RaiseGameOverChanged(true);
    }

    public void Restart()
    {
        _isGameOver = false;
        if (ui != null) ui.SetGameOverVisible(false);

        GameEvents.RaiseGameOverChanged(false);
        
        if (score != null) score.ResetScore();
        if (player != null) player.ResetLives();
        if (waves != null) waves.StartNewGame();
    }
}
