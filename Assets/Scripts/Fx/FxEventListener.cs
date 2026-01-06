using UnityEngine;

public sealed class FxEventListener : MonoBehaviour
{
    [SerializeField] private ExplosionFactory explosionFactory;
    private IExplosionFactory _factory;

    private void Awake()
    {
        if (explosionFactory == null)
        {
            Debug.LogError("ExplosionFactory is not assigned to FxEventListener", this);
            return;
        }

        _factory = explosionFactory;
    }

    private void OnEnable() => GameEvents.ExplosionRequested += OnExplosion;
    private void OnDisable() => GameEvents.ExplosionRequested -= OnExplosion;

    private void OnExplosion(Vector3 pos) => _factory?.Create(pos);
}