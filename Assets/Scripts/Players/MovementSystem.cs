using UnityEngine;

public sealed class MovementSystem : MonoBehaviour
{
    [SerializeField] private CharacterMovementConfig config;
    [SerializeField] private Rigidbody2D m_rigidbody;

    private Vector2 m_currentVelocity;
    private bool m_isGrounded;
    public float MaxHorizontalSpeed => config.MaxHorizontalSpeed;
    public float Gravity { get; set; }

    private void OnValidate()
    {
        if(!m_rigidbody)
        {
            m_rigidbody = GetComponent<Rigidbody2D>();
        }
    }

    private void Awake()
    {
        Gravity = config.GravityScale;
    }

    private void FixedUpdate()
    {
        CheckGrounded();
        HandleCeilingCollision();
        ApplyGravity();
        ClampVerticalSpeed();
        ApplyDrag();

        m_rigidbody.linearVelocity = m_currentVelocity;
    }

    private void CheckGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down,
            config.GroundCheckDistance, config.GroundLayer);
        m_isGrounded = hit.collider is not null;
    }

    private void HandleCeilingCollision()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up,
            config.CeilingCheckDistance, config.GroundLayer);

        if(hit.collider is not null)
        {
            m_currentVelocity.y = 0f;
        }
    }

    private void ApplyGravity()
    {
        if (!m_isGrounded)
        {
            m_currentVelocity.y += Physics2D.gravity.y * config.GravityScale * Time.fixedDeltaTime;
        }
    }

    private void ClampVerticalSpeed()
    {
        if (m_currentVelocity.y < config.MaxFallSpeed)
        {
            m_currentVelocity.y = config.MaxFallSpeed;
        }
    }

    private void ApplyDrag()
    {
        float drag = m_isGrounded ? config.GroundDrag : config.AirDrag;
        m_currentVelocity.x *= (1 - drag);
    }

    public void SetHorizontalVelocity(float velocity)
    {
        float targetSpeed = velocity * config.MaxHorizontalSpeed;
        float accelerate = (Mathf.Abs(targetSpeed) > 0.01f) ? config.Acceleration : config.Deceleration;
        m_currentVelocity.x = Mathf.MoveTowards(m_currentVelocity.x, targetSpeed, accelerate * Time.fixedDeltaTime);
    }

    public void Jump(float jumpForce)
    {
        AddVerticalVelocity(jumpForce);
    }

    public void Dash(Vector2 dashVelocity)
    {
        AddHorizontalVilocity(dashVelocity.x);
    }

    public void AddVerticalVelocity(float velocityDelta) => m_currentVelocity.y += velocityDelta;
    public void AddHorizontalVilocity(float veloityDelta) => m_currentVelocity.x += veloityDelta;

    public bool IsGrounded() => m_isGrounded;

    public void SetGravity(float newGravity)
    {
        if (newGravity < 0.0f)
        {
            newGravity = 0.0f;
        }
        else
        {
            Gravity = newGravity;
        }
    }
}