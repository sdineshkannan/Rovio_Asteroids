using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class WaveManager : MonoBehaviour, IAsteroidEvents
{
    [SerializeField] private PooledAsteroidFactory asteroidFactory;
    [SerializeField] private ScoreManager score;
    [SerializeField] private WaveConfig waveConfig;
    [SerializeField] private SplitRules splitRules;

    private IAsteroidFactory _factory;
    private IAsteroidRecycler _recycler;

    private readonly HashSet<Asteroid> _alive = new();
    private int _wave = 0;
    
    private int _baseAsteroidCount;
    private int _asteroidsPerWaveIncrement;
    private int _maxAsteroidsPerWave;

    public int CurrentWave => _wave;

    private void Awake()
    {
        if (asteroidFactory == null)
        {
            Debug.LogError("AsteroidFactory is not assigned to WaveManager", this);
            return;
        }

        _factory = asteroidFactory;
        _recycler = asteroidFactory;
        
        _baseAsteroidCount = waveConfig != null ? waveConfig.startAsteroids : 4;
        _asteroidsPerWaveIncrement = waveConfig != null ? waveConfig.asteroidsPerWaveIncrement : 1;
        _maxAsteroidsPerWave = waveConfig != null ? waveConfig.maxAsteroidsPerWave : 999;
    }

    public void StartNewGame()
    {
        ClearAll();
        _wave = 0;
        GameEvents.RaiseWaveChanged(_wave);
        NextWave();
    }

    private void ClearAll()
    {
        foreach (var a in _alive)
            if (a != null) _recycler?.Release(a);
        _alive.Clear();
    }

    private IEnumerator NextWaveDelayed()
    {
        float delay = waveConfig != null ? waveConfig.timeBetweenWavesSeconds : 0f;
        if (delay > 0f) yield return new WaitForSeconds(delay);
        NextWave();
    }

    private void NextWave()
    {
        if (_factory == null)
        {
            Debug.LogError("AsteroidFactory is not assigned");
            return;
        }
        
        _wave++;
        GameEvents.RaiseWaveChanged(_wave);
        
        int count = Mathf.Min(_maxAsteroidsPerWave, _baseAsteroidCount + (_wave - 1) * _asteroidsPerWaveIncrement);

        for (int i = 0; i < count; i++)
        {
            var a = _factory.Create(AsteroidSize.Large, null, this);
            _alive.Add(a);
        }
    }
    public void OnAsteroidHit(Asteroid asteroid)
    {
        score.AddFor(asteroid.Size);
        GameEvents.RaiseExplosionRequested(asteroid.transform.position);
        _alive.Remove(asteroid);

        if (_factory == null)
        {
            Debug.LogError("AsteroidFactory is not assigned");
            return;
        }

        if (splitRules != null && splitRules.TryGetRule(asteroid.Size, out var rule))
        {
            for (int i = 0; i < rule.pieces; i++)
            {
                var a = _factory.Create(rule.outputSize, asteroid.transform.position, this);
                a.transform.position = (Vector2)asteroid.transform.position + Random.insideUnitCircle * 0.3f;
                _alive.Add(a);
            }
        }

        if (_alive.Count == 0) StartCoroutine(NextWaveDelayed());
    }
}
