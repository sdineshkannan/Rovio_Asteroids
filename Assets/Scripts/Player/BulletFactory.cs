using UnityEngine;

public sealed class BulletFactory : MonoBehaviour, IBulletFactory
{
    [SerializeField] private BulletPool pool;

    public Bullet Create(Vector2 position, Quaternion rotation, Vector2 direction)
    {
        var b = pool.Spawn();
        b.Fire(position, rotation, direction);
        return b;
    }
}