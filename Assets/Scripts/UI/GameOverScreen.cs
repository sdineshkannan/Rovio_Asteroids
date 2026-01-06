using TMPro;
using UnityEngine;
using UnityEngine.UI;
public sealed class GameOverScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highscoreText;
    [SerializeField] private Button restartButton;
    
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private GameManager gameManager;

    private void Awake()
    {
        if (restartButton != null)
            restartButton.onClick.AddListener(() => gameManager?.Restart());
    }
    private void OnEnable()
    {
        if (scoreManager == null) return;
        
        if (scoreText != null)
        {
            var color = scoreManager.Score < scoreManager.HighScore ? "red" : "green";
            scoreText.text = $"Your Score: <color={color}>{scoreManager.Score}</color>";
        }
        
        if(highscoreText != null) highscoreText.text = $"Highscore: <color=green>{scoreManager.HighScore}</color>";
    }
}
