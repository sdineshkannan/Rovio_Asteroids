using System;
using UnityEngine;
using System.Collections;
using TMPro;

/// <summary>
/// Manages UI elements and events
/// </summary>
public sealed class UIController : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text waveText;
    [SerializeField] private TMP_Text livesText;
    [SerializeField] private TMP_Text waveInfoTxt;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject landingScreen;
    [SerializeField] private GameObject HUD;

    private void OnEnable()
    {
        GameEvents.ScoreChanged += OnScoreChanged;
        GameEvents.WaveChangedStarted += OnWaveChangedStarted;
        GameEvents.LivesChanged += OnLivesChanged;
        GameEvents.GameOverChanged += OnGameOverChanged;
    }

    private void OnDisable()
    {
        GameEvents.ScoreChanged -= OnScoreChanged;
        GameEvents.WaveChangedStarted -= OnWaveChangedStarted;
        GameEvents.LivesChanged -= OnLivesChanged;
        GameEvents.GameOverChanged -= OnGameOverChanged;
    }

    private void Awake()
    {
        if(landingScreen != null) landingScreen.SetActive(true);
    }

    public void ShowGameOverScreen(bool visible)
    {
        if (landingScreen != null) landingScreen.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(visible);
        if (HUD != null) HUD.SetActive(!visible);
    }

    private void OnScoreChanged(int score)
    {
        if (scoreText != null) scoreText.text = $"Score: {score}";
    }

    private void OnWaveChangedStarted(int wave)
    {
        if (waveText != null) waveText.text = $"Wave: {wave}";
        if (waveInfoTxt != null)
        {
            waveInfoTxt.gameObject.SetActive(true);
            waveInfoTxt.text = $"Wave {wave}";
            StartCoroutine(HideWaveInfo());
        }
    }
    
    private IEnumerator HideWaveInfo()
    {
        yield return new WaitForSeconds(2f);
        waveInfoTxt.gameObject.SetActive(false);
    }

    private void OnLivesChanged(int lives)
    {
        if (livesText != null) livesText.text = $"Lives: {lives}";
    }

    private void OnGameOverChanged(bool isGameOver)
    {
        ShowGameOverScreen(isGameOver);
    }
}
