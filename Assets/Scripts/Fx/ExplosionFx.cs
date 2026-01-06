using UnityEngine;

/// <summary>
/// Manages explosion fx and pooling
/// </summary>
[RequireComponent(typeof(Animator))]
public sealed class ExplosionFx : MonoBehaviour, IPoolable
{
    private Animator _anim;
    private ExplosionPool _pool;

    private void Awake() => _anim = GetComponent<Animator>();

    public void Init(ExplosionPool pool) => _pool = pool;

    public void Play(Vector3 position)
    {
        transform.position = position;

        gameObject.SetActive(true);
        _anim.Rebind();
        _anim.Update(0f);
    }

    public void OnAnimationFinished()
    {
        if (_pool != null) _pool.Release(this);
        else gameObject.SetActive(false);
    }
    
    public void OnReturnedToPool()
    {
    }

    public void OnTakenFromPool()
    {
    }
}