using UnityEngine;

public sealed class PlayerShooter : MonoBehaviour
{
    [SerializeField] private PlayerConfig config;
    [SerializeField] private BulletFactory bulletFactory;

    [SerializeField] private Transform muzzle;

    private IBulletFactory _factory;
    private float _nextFireTime;

    private void Awake()
    {
        if (bulletFactory == null)
        {
            Debug.LogError("BulletFactory is not assigned to PlayerShooter", this);
            return;
        }

        _factory = bulletFactory;
    }
    
    private void Update()
    {
        if (Time.time < _nextFireTime) return;

        if (_factory == null)
        {
            Debug.LogError("BulletFactory is not assigned");
            return;
        }
        
        if (Input.GetKey(KeyCode.Space))
        {
            _factory.Create(muzzle.position, muzzle.rotation, muzzle.up);

            _nextFireTime = Time.time + config.fireCooldown;
        }
    }
}
