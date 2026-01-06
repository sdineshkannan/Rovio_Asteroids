using UnityEngine;

public sealed class ScreenBounds
{
    public Vector2 Min { get; }
    public Vector2 Max { get; }

    public ScreenBounds(Camera cam, float padding = 0f)
    {
        var bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        var topRight   = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));

        Min = new Vector2(bottomLeft.x - padding, bottomLeft.y - padding);
        Max = new Vector2(topRight.x + padding,   topRight.y + padding);
    }
}
