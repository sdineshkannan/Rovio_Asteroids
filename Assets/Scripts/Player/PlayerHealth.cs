using System;
using UnityEngine;

/// <summary>
/// Controls player health
/// </summary>
public sealed class PlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerConfig config;
    [SerializeField] private PlayerStateController stateController;
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    private int _lives;
    private float _invulnUntil;
    private Collider2D _collider;
    private float _nextFlashTime;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        GameEvents.GameOverChanged += OnGameOverChanged;
        GameEvents.WaveChanged += EnableInvulnerability;
    }

    private void OnDisable()
    {
        GameEvents.GameOverChanged -= OnGameOverChanged;
        GameEvents.WaveChanged -= EnableInvulnerability;
    }

    private void OnGameOverChanged(bool isGameOver)
    {
        if (!isGameOver) ResetLives();
    }

    private void Update()
    {
        if (_invulnUntil > 0f && _lives > 0)
        {
            if (Time.time >= _invulnUntil)
            {
                _invulnUntil = 0f;
                EnableCollision();
                SetSpriteVisible(true);
            }
            else
            {
                UpdateFlashing();
            }
        }
    }

    public void ResetLives()
    {
        _lives = config != null ? config.startLives : 3;
        _invulnUntil = 0;
        EnableCollision();
        
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
        EnableInvulnerability();

        if (_lives <= 0)
        {
            GameEvents.RaiseExplosionRequested(transform.position);
            stateController.SetState(PlayerState.Dead);
            GameEvents.RaiseGameOverRequested();
        }
        else
        {
            GameEvents.RaiseExplosionRequested(transform.position);
            DisableCollision();
            stateController.SetState(PlayerState.Disabled);
            stateController.SetState(PlayerState.Active);
        }
    }

    private void DisableCollision()
    {
        if (_collider != null) _collider.enabled = false;
    }

    private void EnableCollision()
    {
        if (_collider != null) _collider.enabled = true;
    }

    private void UpdateFlashing()
    {
        if (Time.time >= _nextFlashTime)
        {
            float flashRate = config != null ? config.invulnFlashRate : 0.1f;
            _nextFlashTime = Time.time + flashRate;
            
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = !spriteRenderer.enabled;
            }
        }
    }

    private void SetSpriteVisible(bool visible)
    {
        if (spriteRenderer != null) spriteRenderer.enabled = visible;
    }
    
    private void EnableInvulnerability()
    {
        _invulnUntil = Time.time + (config != null ? config.invulnSeconds : 1f);
    }
}
