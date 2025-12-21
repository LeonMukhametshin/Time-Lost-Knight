using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_TextMeshProUGUI;

    [Header("Data")]
    public PlayerData Data;

    [Header("Runtime State")]
    public Rigidbody2D m_rigidbody { get; private set; }

    [SerializeField] private CharacterInputController m_input;
    public Vector2 MoveInput { get; set; }

    public bool IsFacingRight { get; private set; } = true;
    public bool IsJumping { get; set; }
    public bool IsDashing { get; set; }
    public bool IsDashAttacking { get; set; }

    public int DashesLeft { get; set; }

    public float LastOnGroundTime { get; set; }
    public float LastPressedJumpTime { get; set; }
    public float LastPressedDashTime { get; set; }

    [SerializeField] private CoroutineRunner m_runner;

    [SerializeField] private CharacterInputObserver m_observer;
    [SerializeField] private BoxCollider2D m_collider;

    private AbilitiesContainer m_abilitiesContainer;

    private float m_originalGravityScale;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();

        DashesLeft = Data.MaxDashes;

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

        m_rigidbody.gravityScale = Data.GravityScale;
        m_originalGravityScale = m_rigidbody.gravityScale;

        m_rigidbody.sharedMaterial = Data.BaseMaterial;
    }

    private void Update()
    {
        UpdateTimers();

        m_abilitiesContainer.UpdateAllAbilities();

        m_TextMeshProUGUI.text = m_rigidbody.linearVelocity.ToString();

        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            m_abilitiesContainer.ActivateAbility("Dash");
            Debug.Log("Dash activated");
        }

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            m_abilitiesContainer.DeactivateAbility("Dash");
            Debug.Log("Dash deactivated");
        }

        UpdateGravityScale();
    }

    private void UpdateGravityScale()
    {
        if (IsGrounded())
        {
            m_rigidbody.gravityScale = m_originalGravityScale;
            return;
        }

        if (m_rigidbody.linearVelocityY < 0)
        {
            m_rigidbody.gravityScale = m_originalGravityScale * Data.FallGravityMultiplier;
        }
        else
        {
            m_rigidbody.gravityScale = m_originalGravityScale;
        }
    }

    private void ClampFallSpeed()
    {
        if (m_rigidbody.linearVelocityY < Data.MaxFallSpeed)
        {
            Vector2 velocity = m_rigidbody.linearVelocity;
            velocity.y = Data.MaxFallSpeed;
            m_rigidbody.linearVelocity = velocity;
        }
    }

    private void FixedUpdate()
    {
        ClampFallSpeed();
        CheckGrounded();
    }

    private void UpdateTimers()
    {
        LastOnGroundTime -= Time.deltaTime;
        LastPressedJumpTime -= Time.deltaTime;
        LastPressedDashTime -= Time.deltaTime;
    }

    private void CheckGrounded()
    {
        if (IsGrounded())
        {
            LastOnGroundTime = Data.CoyoteTime;
            IsJumping = false;
            DashesLeft = Data.MaxDashes;

            m_rigidbody.sharedMaterial = Data.BaseMaterial;
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
            Data.GroundCheckDistance,
            Data.GroundLayer
        );
    }

    public void Flip(float xInput)
    {
        if (xInput == 0) return;

        if (xInput > 0 && !IsFacingRight)
        {
            FlipInternal();
        }
        else if (xInput < 0 && IsFacingRight)
        {
            FlipInternal();
        }
    }

    private void FlipInternal()
    {
        IsFacingRight = !IsFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}