using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Asteroids/Asteroid Type Database")]
public sealed class AsteroidTypeDatabase : ScriptableObject
{
    public AsteroidTypeConfig[] types;

    public AsteroidTypeConfig GetDefaultForSize(AsteroidSize size)
    {
        foreach (var t in types)
            if (t != null && t.size == size && t.typeId == "Default")
                return t;

        // fallback: first match by size
        foreach (var t in types)
            if (t != null && t.size == size)
                return t;

        Debug.LogError($"No AsteroidTypeConfig found for size {size}");
        return null;
    }
}