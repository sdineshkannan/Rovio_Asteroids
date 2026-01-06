using UnityEngine;

public interface IAsteroidFactory
{
    Asteroid Create(AsteroidSize size, Vector2? atPosition, IAsteroidEvents eventsSink);
}