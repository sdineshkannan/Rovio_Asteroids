using System.Collections.Generic;
using UnityEngine;

public sealed class PooledAsteroidFactory : MonoBehaviour, IAsteroidFactory, IAsteroidRecycler
{
    [Header("Shared prefab (used when type.prefabOverride is null)")]
    [SerializeField] private Asteroid sharedPrefab;

    [Header("Type database")]
    [SerializeField] private AsteroidTypeDatabase typeDatabase;

    [Header("Wave config (for spawn padding)")]
    [SerializeField] private WaveConfig waveConfig;

    private Camera _cam;
    private ScreenBounds _cachedBounds;
    private float _lastPadding = -1f;

    // Pools per type config (best separation)
    private readonly Dictionary<AsteroidTypeConfig, ObjectPool<Asteroid>> _pools = new();

    private void Awake()
    {
        _cam = Camera.main;
        PrewarmAll();
    }

    private void PrewarmAll()
    {
        if (typeDatabase == null || typeDatabase.types == null) return;

        foreach (var type in typeDatabase.types)
        {
            if (type == null) continue;
            EnsurePool(type);
        }
    }

    private ObjectPool<Asteroid> EnsurePool(AsteroidTypeConfig type)
    {
        if (_pools.TryGetValue(type, out var pool))
            return pool;

        var prefab = type.prefabOverride != null ? type.prefabOverride : sharedPrefab;
        if (prefab == null)
        {
            Debug.LogError($"Missing prefab for type '{type.name}'. Set sharedPrefab or prefabOverride.");
            return null;
        }

        pool = new ObjectPool<Asteroid>(prefab, Mathf.Max(0, type.prewarmCount), parent: transform);
        _pools[type] = pool;
        return pool;
    }

    public Asteroid Create(AsteroidSize size, Vector2? atPosition, IAsteroidEvents eventsSink)
    {
        var type = typeDatabase.GetDefaultForSize(size);
        if (type == null) return null;

        var pool = EnsurePool(type);
        if (pool == null) return null;

        float padding = waveConfig != null ? waveConfig.spawnPadding : 1.5f;
        
        if (_cachedBounds == null || _lastPadding != padding)
        {
            _cachedBounds = new ScreenBounds(_cam, padding);
            _lastPadding = padding;
        }

        Vector2 pos = atPosition ?? new Vector2(
            Random.Range(_cachedBounds.Min.x, _cachedBounds.Max.x),
            Random.Range(_cachedBounds.Min.y, _cachedBounds.Max.y)
        );

        var a = pool.Get();
        a.gameObject.SetActive(true);

        a.transform.position = pos;
        a.transform.rotation = Quaternion.identity;

        a.SetSize(size);

        Vector2 dir = Random.insideUnitCircle.normalized;
        a.Init(dir, type, eventsSink, recycler: this);

        return a;
    }

    public void Release(Asteroid asteroid)
    {
        if (asteroid == null) return;

        var type = typeDatabase.GetDefaultForSize(asteroid.Size);
        if (type == null || !_pools.TryGetValue(type, out var pool))
        {
            asteroid.gameObject.SetActive(false);
            asteroid.transform.SetParent(transform);
            return;
        }

        pool.Return(asteroid);
    }
}
