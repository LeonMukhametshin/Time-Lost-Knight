using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CoroutineRunner m_runner;

    [SerializeField] private CharacterInputObserver m_observer;
    [SerializeField] private BoxCollider2D m_collider;

    [Header("Data")]
    [field: SerializeField] public PlayerData data { get; private set; }

    [Header("Runtime State")]
    [field: SerializeField] public Rigidbody2D rigidbody { get; private set; }

    [SerializeField] private PlayerInputHandler m_input;
    public Vector2 moveInput { get; set; }

    public bool isJumping { get; set; }
    public bool isDashing { get; set; }
    public bool isDashAttacking { get; set; }
    public bool isFacingRight { get; private set; } = true;

    public int dashesLeft { get; set; }

    public float lastOnGroundTime { get; set; }
    public float lastPressedJumpTime { get; set; }
    public float lastPressedDashTime { get; set; }

    private AbilitiesContainer m_abilitiesContainer;

    private float m_originalGravityScale;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        dashesLeft = data.MaxDashes;

        m_abilitiesContainer = new AbilitiesContainer();

        var walk = new WalkAbility(this);
        var jump = new JumpAbility(this);
        var dash = new DashAbility(this, m_runner);

        m_observer = new CharacterInputObserver(
            walk,
            jump,
            dash,
            m_input);

        m_observer.Subscribe();

        m_abilitiesContainer.RegisterAbility(walk, "Walk");
        m_abilitiesContainer.RegisterAbility(jump, "Jump");
        m_abilitiesContainer.RegisterAbility(dash, "Dash");

        rigidbody.gravityScale = data.GravityScale;
        m_originalGravityScale = rigidbody.gravityScale;

        rigidbody.sharedMaterial = data.BaseMaterial;
    }

    private void Update()
    {
        UpdateTimers();
        m_abilitiesContainer.UpdateAllAbilities();
        UpdateGravityScale();
    }

    private void FixedUpdate()
    {
        ClampFallSpeed();
        CheckGrounded();
    }

    private void UpdateGravityScale()
    {
        if (IsGrounded())
        {
            rigidbody.gravityScale = m_originalGravityScale;
            return;
        }

        if (rigidbody.linearVelocityY < 0)
        {
            rigidbody.gravityScale = m_originalGravityScale * data.FallGravityMultiplier;
        }
        else
        {
            rigidbody.gravityScale = m_originalGravityScale;
        }
    }

    private void ClampFallSpeed()
    {
        if (rigidbody.linearVelocityY < data.MaxFallSpeed)
        {
            Vector2 velocity = rigidbody.linearVelocity;
            velocity.y = data.MaxFallSpeed;
            rigidbody.linearVelocity = velocity;
        }
    }

    private void UpdateTimers()
    {
        lastOnGroundTime -= Time.deltaTime;
        lastPressedJumpTime -= Time.deltaTime;
        lastPressedDashTime -= Time.deltaTime;
    }

    private void CheckGrounded()
    {
        if (IsGrounded())
        {
            lastOnGroundTime = data.CoyoteTime;
            isJumping = false;
            dashesLeft = data.MaxDashes;

            rigidbody.sharedMaterial = data.BaseMaterial;
        }
    }

    private bool IsGrounded()
    {
        Bounds bounds = m_collider.bounds;

        float skinWidth = 0.02f;

        Vector2 boxSize = new Vector2(
            bounds.size.x - skinWidth,
            bounds.size.y
        );

        return Physics2D.BoxCast(
            bounds.center,
            boxSize,
            0f,
            Vector2.down,
            data.GroundCheckDistance,
            data.GroundLayer
        );
    }

    public void Flip(float xInput)
    {
        if (xInput == 0) return;

        if (xInput > 0 && !isFacingRight)
        {
            FlipInternal();
        }
        else if (xInput < 0 && isFacingRight)
        {
            FlipInternal();
        }
    }

    private void FlipInternal()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}