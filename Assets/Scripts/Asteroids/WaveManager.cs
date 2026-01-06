using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Manages wave logic and asteroid spawning
/// </summary>
public sealed class WaveManager : MonoBehaviour, IAsteroidEvents
{
    [SerializeField] private PooledAsteroidFactory asteroidFactory;
    [SerializeField] private WaveConfig waveConfig;
    [SerializeField] private SplitRules splitRules;
    [SerializeField] private PlayerStateController playerStateController;
    
    private IAsteroidFactory _factory;
    private IAsteroidRecycler _recycler;

    private readonly HashSet<Asteroid> _alive = new();
    private int _wave = 0;
    
    private int _baseAsteroidCount;
    private int _asteroidsPerWaveIncrement;
    private int _maxAsteroidsPerWave;

    private Coroutine _waveRoutine;

    public int CurrentWave => _wave;

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
        if (!isGameOver) StartNewGame();
    }

    private void Awake()
    {
        if (asteroidFactory == null)
        {
            Debug.LogError("AsteroidFactory is not assigned to WaveManager", this);
            return;
        }

        _factory = asteroidFactory;
        _recycler = asteroidFactory;
        
        // Initialize wave variables once
        _baseAsteroidCount = waveConfig != null ? waveConfig.startAsteroids : 4;
        _asteroidsPerWaveIncrement = waveConfig != null ? waveConfig.asteroidsPerWaveIncrement : 1;
        _maxAsteroidsPerWave = waveConfig != null ? waveConfig.maxAsteroidsPerWave : 999;
    }

    public void StartNewGame()
    {
        ClearAll();
        _wave = 0;
        
        playerStateController.SetState(PlayerState.Disabled);

        if (_waveRoutine != null)
            StopCoroutine(_waveRoutine);

        _waveRoutine = StartCoroutine(WaveStartRoutine());
    }
    
    private IEnumerator WaveStartRoutine()
    {
        GameEvents.RaiseWaveChangeStart(_wave + 1);
        playerStateController.SetState(PlayerState.Disabled);

        float delay = waveConfig != null ? waveConfig.timeBetweenWavesSeconds : 0f;
        if (delay > 0f) yield return new WaitForSeconds(delay);
        
        GameEvents.RaiseWaveChanged();
        NextWave();
        playerStateController.SetState(PlayerState.Active);
    }

    private void NextWave()
    {
        if (_factory == null)
        {
            Debug.LogError("AsteroidFactory is not assigned");
            return;
        }
        
        _wave++;
        
        int count = Mathf.Min(_maxAsteroidsPerWave, _baseAsteroidCount + (_wave - 1) * _asteroidsPerWaveIncrement);

        for (int i = 0; i < count; i++)
        {
            var a = _factory.Create(AsteroidSize.Large, null, this);
            _alive.Add(a);
        }
    }
    public void OnAsteroidHit(Asteroid asteroid)
    {
        GameEvents.RaiseScoreAwarded(asteroid.Type.score);
        GameEvents.RaiseExplosionRequested(asteroid.transform.position);
        _alive.Remove(asteroid);

        if (_factory == null)
        {
            Debug.LogError("AsteroidFactory is not assigned");
            return;
        }

        // Split asteroid if it has a split rule
        if (splitRules != null && splitRules.TryGetRule(asteroid.Size, out var rule))
        {
            for (int i = 0; i < rule.pieces; i++)
            {
                var a = _factory.Create(rule.outputSize, asteroid.transform.position, this);
                a.transform.position = (Vector2)asteroid.transform.position + Random.insideUnitCircle * 0.3f;
                _alive.Add(a);
            }
        }

        // If all asteroids are destroyed, start next wave
        if (_alive.Count == 0) StartCoroutine(WaveStartRoutine());
    }
    
    private void ClearAll()
    {
        foreach (var a in _alive)
            if (a != null) _recycler?.Release(a);
        _alive.Clear();
    }
}
