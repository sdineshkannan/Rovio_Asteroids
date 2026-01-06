using UnityEngine;

public sealed class BulletPool : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private PlayerConfig config;

    private ObjectPool<Bullet> _pool;

    private void Awake()
    {
        int prewarm = config != null ? config.bulletPoolPrewarm : 30;
        _pool = new ObjectPool<Bullet>(bulletPrefab, prewarm, parent: transform);
    }

    public Bullet Spawn()
    {
        var b = _pool.Get();
        b.Init(this, config);
        return b;
    }

    public void Release(Bullet b) => _pool.Return(b);
}