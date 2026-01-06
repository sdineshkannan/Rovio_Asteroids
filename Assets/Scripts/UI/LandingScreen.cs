using System;
using UnityEngine;
using UnityEngine.UI;
public class LandingScreen : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private GameManager gameManager;
    private void Awake()
    {
        if (playButton != null)
            playButton.onClick.AddListener(() => gameManager?.Restart());
    }
}
