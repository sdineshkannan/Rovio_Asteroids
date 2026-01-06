using UnityEngine;

[CreateAssetMenu(menuName = "Asteroids/Split Config")]
public sealed class SplitConfig : ScriptableObject
{
    public AsteroidSize inputSize;
    public bool canSplit = true;
    public AsteroidSize outputSize;
    public int pieces = 2;
}