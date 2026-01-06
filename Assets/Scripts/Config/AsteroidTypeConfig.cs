using UnityEngine;

[CreateAssetMenu(menuName = "Asteroids/Asteroid Type Config")]
public sealed class AsteroidTypeConfig : ScriptableObject
{
    [Header("ID")]
    public string typeId = "Default";

    [Header("Size")]
    public AsteroidSize size;

    [Header("Presentation")]
    public Sprite sprite;
    public float scale = 1f;

    [Header("Movement")]
    public float minSpeed = 1.5f;
    public float maxSpeed = 3.0f;
    public float minAngularVelocity = -90f;
    public float maxAngularVelocity = 90f;

    [Header("Scoring")]
    public int score = 10;

    [Header("Pooling")]
    public int prewarmCount = 10;

    [Header("Prefab override (optional)")]
    public Asteroid prefabOverride;
}