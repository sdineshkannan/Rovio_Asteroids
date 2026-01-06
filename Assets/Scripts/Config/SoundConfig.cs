using UnityEngine;

[CreateAssetMenu(menuName = "Asteroids/Sound Config")]
public sealed class SoundConfig : ScriptableObject
{
    [Header("Shoot")]
    public AudioClip shootClip;
    [Range(0f, 1f)] public float shootVolume = 0.8f;

    [Header("Explosion")]
    public AudioClip explosionClip;
    [Range(0f, 1f)] public float explosionVolume = 0.8f;
}