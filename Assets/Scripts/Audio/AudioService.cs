using UnityEngine;

public sealed class AudioService : MonoBehaviour
{
    [SerializeField] private SoundConfig config;

    private AudioSource _source;

    private void Awake()
    {
        _source = gameObject.AddComponent<AudioSource>();
        _source.playOnAwake = false;
        _source.loop = false;
        _source.spatialBlend = 0f; // 2D audio
    }

    private void OnEnable()
    {
        GameEvents.ShootRequested += PlayShoot;
        GameEvents.ExplosionRequested += PlayExplosion;
    }

    private void OnDisable()
    {
        GameEvents.ShootRequested -= PlayShoot;
        GameEvents.ExplosionRequested -= PlayExplosion;
    }

    private void PlayShoot()
    {
        if (config?.shootClip == null) return;
        _source.PlayOneShot(config.shootClip, config.shootVolume);
    }

    private void PlayExplosion(Vector3 _)
    {
        if (config?.explosionClip == null) return;
        _source.PlayOneShot(config.explosionClip, config.explosionVolume);
    }
}