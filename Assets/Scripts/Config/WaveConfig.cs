using UnityEngine;

[CreateAssetMenu(menuName = "Asteroids/Wave Config")]
public sealed class WaveConfig : ScriptableObject
{
    [Header("Wave Progression")]
    public int startAsteroids = 4;
    public int asteroidsPerWaveIncrement = 1;
    public int maxAsteroidsPerWave = 12;

    [Header("Wave Timing")]
    public float timeBetweenWavesSeconds = 0.5f;

    [Header("Spawning")]
    public float spawnPadding = 1.5f;
}