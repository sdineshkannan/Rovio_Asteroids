using UnityEngine;

/// <summary>
/// Manages player states
/// It can be used for animations and other state based logic 
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public sealed class PlayerStateController : MonoBehaviour
{
    public PlayerState CurrentState { get; private set; } = PlayerState.Disabled;

    private Rigidbody2D _rb;
    [SerializeField] private SpriteRenderer playerSprite;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void SetState(PlayerState newState)
    {
        if (CurrentState == newState) return;

        CurrentState = newState;

        if(playerSprite != null) playerSprite.enabled = CurrentState != PlayerState.Dead;

        if (CurrentState == PlayerState.Disabled)
        {
            _rb.linearVelocity = Vector2.zero;
            _rb.angularVelocity = 0f;
            transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        }

        GameEvents.RaisePlayerStateChanged(newState);
    }

    public bool IsActive => CurrentState == PlayerState.Active;
}