using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages game over screen logic
/// </summary>
public sealed class GameOverScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highscoreText;
    [SerializeField] private Button restartButton;
    
    private int _currentScore;
    private int _highScore;

    private void Awake()
    {
        if (restartButton != null)
            restartButton.onClick.AddListener(() => GameEvents.RaiseGameOverChanged(false));
    }

    private void OnEnable()
    {
        GameEvents.GameOverWithScore += OnGameOverWithScore;
    }

    private void OnDisable()
    {
        GameEvents.GameOverWithScore -= OnGameOverWithScore;
    }

    private void OnGameOverWithScore(int score, int highScore)
    {
        _currentScore = score;
        _highScore = highScore;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        if (scoreText != null)
        {
            var color = _currentScore < _highScore ? "red" : "green";
            scoreText.text = $"Your Score: <color={color}>{_currentScore}</color>";
        }
        
        if(highscoreText != null) highscoreText.text = $"Highscore: <color=green>{_highScore}</color>";
    }
}
