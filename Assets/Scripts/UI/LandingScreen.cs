using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages landing screen logic
/// </summary>
public sealed class LandingScreen : MonoBehaviour
{
    [SerializeField] private Button playButton;

    private void Awake()
    {
        if (playButton != null)
            playButton.onClick.AddListener(() => GameEvents.RaiseGameOverChanged(false));
    }
}
