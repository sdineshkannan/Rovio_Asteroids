using UnityEngine;

/// <summary>
/// Controls player movement and rotation
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public sealed class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerConfig config;
    [SerializeField] private PlayerStateController stateController;
    
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        if (config != null)
        {
            _rb.linearDamping = config.linearDrag;
            _rb.angularDamping = config.angularDrag;
        }
    }

    private void FixedUpdate()
    {
        if (!stateController.IsActive || config == null) return;

        float turn = -Input.GetAxisRaw("Horizontal");
        float thrust = Mathf.Clamp01(Input.GetAxisRaw("Vertical"));

        _rb.MoveRotation(_rb.rotation + turn * config.turnSpeedDeg * Time.fixedDeltaTime);

        if (thrust > 0f) _rb.AddForce(transform.up * (config.thrustForce * thrust), ForceMode2D.Force);

        if (_rb.linearVelocity.magnitude > config.maxSpeed) _rb.linearVelocity = _rb.linearVelocity.normalized * config.maxSpeed;
    }
}
