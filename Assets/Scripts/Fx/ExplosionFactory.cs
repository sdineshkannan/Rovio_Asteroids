using UnityEngine;

public sealed class ExplosionFactory : MonoBehaviour, IExplosionFactory
{
    [SerializeField] private ExplosionPool pool;

    public void Create(Vector3 position) => pool.Spawn(position);
}