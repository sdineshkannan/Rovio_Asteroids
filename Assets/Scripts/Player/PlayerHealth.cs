using System;
using UnityEngine;

public sealed class PlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerConfig config;
    [SerializeField] private GameManager gameManager;
    
    private int _lives;
    private float _invulnUntil;
    private Rigidbody2D _rb;
    private void Awake() => _rb = GetComponent<Rigidbody2D>();

    public void ResetLives()
    {
        _lives = config != null ? config.startLives : 3;
        _invulnUntil = 0f;
        transform.position = Vector3.zero;
        gameObject.SetActive(true);
        GameEvents.RaiseLivesChanged(_lives);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Time.time < _invulnUntil) return;
        if (collision.collider.GetComponent<Asteroid>() == null) return;

        _lives--;
        GameEvents.RaiseLivesChanged(_lives);
        
        _invulnUntil = Time.time + (config != null ? config.invulnSeconds : 1f);

        if (_lives <= 0)
        {
            GameEvents.RaiseExplosionRequested(transform.position);

            gameObject.SetActive(false);
            gameManager?.GameOver();
        }
        else
        {
            GameEvents.RaiseExplosionRequested(transform.position);
            transform.position = Vector3.zero;
            if (_rb != null)
            {
                _rb.linearVelocity = Vector2.zero;
                _rb.angularVelocity = 0f;
            }
        }
    }
}
