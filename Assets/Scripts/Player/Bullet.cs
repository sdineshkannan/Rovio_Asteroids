using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public sealed class Bullet : MonoBehaviour, IPoolable
{
    private Rigidbody2D _rb;
    private float _dieAt;

    private BulletPool _pool;
    private PlayerConfig _config;
    private void Awake() => _rb = GetComponent<Rigidbody2D>();

    public void Init(BulletPool pool, PlayerConfig config)
    {
        _pool = pool;
        _config = config;
    }

    public void Fire(Vector2 position, Quaternion rotation, Vector2 direction)
    {
        transform.SetPositionAndRotation(position, rotation);

        _rb.linearVelocity = direction.normalized * _config.bulletSpeed;
        _rb.angularVelocity = 0f;

        _dieAt = Time.time + _config.bulletLifeSeconds;
    }

    public void HitSomething() => _pool.Release(this);

    private void Update()
    {
        if (Time.time >= _dieAt)
            _pool.Release(this);
    }

    public void OnReturnedToPool()
    {
        if (_rb != null)
        {
            _rb.linearVelocity = Vector2.zero;
            _rb.angularVelocity = 0f;
        }
    }

    public void OnTakenFromPool()
    {
    }
}