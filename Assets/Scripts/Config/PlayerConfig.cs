using UnityEngine;

[CreateAssetMenu(menuName = "Asteroids/Player Config")]
public sealed class PlayerConfig : ScriptableObject
{
    [Header("Movement")]
    public float thrustForce = 8f;
    public float turnSpeedDeg = 220f;
    public float maxSpeed = 10f;
    public float linearDrag = 0f;
    public float angularDrag = 2f;

    [Header("Shooting")]
    public float fireCooldown = 0.15f;
    public float bulletSpeed = 16f;
    public float bulletLifeSeconds = 1.3f;
    public int bulletPoolPrewarm = 30;

    [Header("Survivability")]
    public int startLives = 3;
    public float invulnSeconds = 1.0f;
    public float invulnFlashRate = 0.1f;
}