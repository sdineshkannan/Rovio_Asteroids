using System;
using UnityEngine;

/// <summary>
/// Controls player health
/// </summary>
public sealed class PlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerConfig config;
    [SerializeField] private PlayerStateController stateController;
    
    private int _lives;
    private float _invulnUntil;

    private void OnEnable()
    {
        GameEvents.GameOverChanged += OnGameOverChanged;
    }

    private void OnDisable()
    {
        GameEvents.GameOverChanged -= OnGameOverChanged;
    }

    private void OnGameOverChanged(bool isGameOver)
    {
        if (!isGameOver) ResetLives();
    }

    public void ResetLives()
    {
        _lives = config != null ? config.startLives : 3;
        _invulnUntil = 0f;
        
        stateController.SetState(PlayerState.Disabled);
        
        GameEvents.RaiseLivesChanged(_lives);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (stateController.CurrentState != PlayerState.Active) return;
        if (Time.time < _invulnUntil) return;
        if (collision.collider.GetComponent<Asteroid>() == null) return;

        _lives--;
        GameEvents.RaiseLivesChanged(_lives);
        
        _invulnUntil = Time.time + (config != null ? config.invulnSeconds : 1f);

        if (_lives <= 0)
        {
            GameEvents.RaiseExplosionRequested(transform.position);
            stateController.SetState(PlayerState.Dead);
            GameEvents.RaiseGameOverRequested();
        }
        else
        {
            GameEvents.RaiseExplosionRequested(transform.position);
            stateController.SetState(PlayerState.Disabled);
            stateController.SetState(PlayerState.Active);
        }
    }
}
