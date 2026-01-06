using UnityEngine;

public sealed class ScreenWrapper : MonoBehaviour
{
    [SerializeField] private float padding = 0.5f;
    private Camera _cam;

    private void Awake() => _cam = Camera.main;

    private void LateUpdate()
    {
        var b = new ScreenBounds(_cam, padding);
        var p = transform.position;

        if (p.x < b.Min.x) p.x = b.Max.x;
        else if (p.x > b.Max.x) p.x = b.Min.x;

        if (p.y < b.Min.y) p.y = b.Max.y;
        else if (p.y > b.Max.y) p.y = b.Min.y;

        transform.position = p;
    }
}
