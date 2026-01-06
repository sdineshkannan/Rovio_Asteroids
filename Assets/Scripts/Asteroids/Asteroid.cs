using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public sealed class Asteroid : MonoBehaviour, IPoolable
{
    [SerializeField] private AsteroidSize size = AsteroidSize.Large;

    private Rigidbody2D _rb;
    private SpriteRenderer _sr;

    private IAsteroidEvents _events;
    private IAsteroidRecycler _recycler;

    public AsteroidSize Size => size;

    private AsteroidTypeConfig _type;
    public AsteroidTypeConfig Type => _type;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
    }

    public void SetSize(AsteroidSize s) => size = s;

    public void Init(Vector2 dir, AsteroidTypeConfig type, IAsteroidEvents eventsSink, IAsteroidRecycler recycler)
    {
        _events = eventsSink;
        _recycler = recycler;
        _type = type;
        
        if (_sr != null && type.sprite != null) _sr.sprite = type.sprite;
        transform.localScale = Vector3.one * type.scale;

        // movement
        float speed = Random.Range(type.minSpeed, type.maxSpeed);
        _rb.linearVelocity = dir.normalized * speed;

        _rb.angularVelocity = Random.Range(type.minAngularVelocity, type.maxAngularVelocity);
    }

    public void OnReturnedToPool()
    {
        _events = null;
        _recycler = null;

        if (_rb != null)
        {
            _rb.linearVelocity = Vector2.zero;
            _rb.angularVelocity = 0f;
        }
    }

    public void OnTakenFromPool()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent<Bullet>(out var bullet)) return;

        bullet.HitSomething(); 
        _events?.OnAsteroidHit(this);
        _recycler?.Release(this);
    }
}