using UnityEngine;

public sealed class ExplosionPool : MonoBehaviour
{
    [SerializeField] private ExplosionFx explosionPrefab;
    [SerializeField] private int prewarm = 20;

    private ObjectPool<ExplosionFx> _pool;

    private void Awake() => _pool = new ObjectPool<ExplosionFx>(explosionPrefab, prewarm, parent: transform);

    public void Spawn(Vector3 pos)
    {
        var fx = _pool.Get();
        fx.Init(this);
        fx.Play(pos);
    }

    public void Release(ExplosionFx fx) => _pool.Return(fx);
}