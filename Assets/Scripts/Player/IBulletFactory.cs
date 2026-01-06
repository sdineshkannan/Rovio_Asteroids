using UnityEngine;

public interface IBulletFactory
{
    Bullet Create(Vector2 position, Quaternion rotation, Vector2 direction);
}