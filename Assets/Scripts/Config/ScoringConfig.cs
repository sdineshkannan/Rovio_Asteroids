using UnityEngine;

[CreateAssetMenu(menuName = "Asteroids/Scoring Config")]
public sealed class ScoringConfig : ScriptableObject
{
    public int largePoints = 20;
    public int mediumPoints = 50;
    public int smallPoints = 100;

    public int GetPoints(AsteroidSize size) => size switch
    {
        AsteroidSize.Large => largePoints,
        AsteroidSize.Medium => mediumPoints,
        AsteroidSize.Small => smallPoints,
        _ => 0
    };
}