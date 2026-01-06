using UnityEngine;
using UnityEngine.UI;
using TMPro;

public sealed class UIController : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text waveText;
    [SerializeField] private TMP_Text livesText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject HUD;

    private void OnEnable()
    {
        GameEvents.ScoreChanged += OnScoreChanged;
        GameEvents.WaveChanged += OnWaveChanged;
        GameEvents.LivesChanged += OnLivesChanged;
        GameEvents.GameOverChanged += OnGameOverChanged;
    }

    private void OnDisable()
    {
        GameEvents.ScoreChanged -= OnScoreChanged;
        GameEvents.WaveChanged -= OnWaveChanged;
        GameEvents.LivesChanged -= OnLivesChanged;
        GameEvents.GameOverChanged -= OnGameOverChanged;
    }
    public void SetGameOverVisible(bool visible)
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(visible);
        if (HUD != null) HUD.SetActive(!visible);
    }

    private void OnScoreChanged(int score)
    {
        if (scoreText != null) scoreText.text = $"Score: {score}";
    }

    private void OnWaveChanged(int wave)
    {
        if (waveText != null) waveText.text = $"Wave: {wave}";
    }

    private void OnLivesChanged(int lives)
    {
        if (livesText != null) livesText.text = $"Lives: {lives}";
    }

    private void OnGameOverChanged(bool isGameOver)
    {
        SetGameOverVisible(isGameOver);
    }
}
