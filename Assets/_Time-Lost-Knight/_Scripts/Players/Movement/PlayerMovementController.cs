using UnityEngine;

public class PlayerMovementController : PlayerSystemBase
{
    [Header("References")]
    [SerializeField] private Rigidbody2D m_rigidbody;
    [SerializeField] private BoxCollider2D m_collider;

    [Header("Config")]
    [SerializeField] private PlayerData m_data;

    public MovementState state { get; set; }
    public Rigidbody2D rigidbody => m_rigidbody;

    private GroundChecker m_groundChecker;
    private GravityController m_gravity;
    private FacingController m_facing;

    protected override void OnInitialize()
    {
        m_rigidbody.gravityScale = m_data.GravityScale;
        m_rigidbody.sharedMaterial = m_data.BaseMaterial;

        state = new MovementState(m_data.MaxDashes);

        m_groundChecker = new GroundChecker(m_collider, m_data);
        m_gravity = new GravityController(m_rigidbody, m_data);
        m_facing = new FacingController(transform, state);
    }

    private void Update()
    {
        state.Tick(Time.deltaTime);
        m_gravity.Update(m_groundChecker.IsGrounded());
    }

    private void FixedUpdate()
    {
        m_gravity.ClampFallSpeed();

        if (m_groundChecker.IsGrounded())
        {
            state.RefreshGrounded(m_data.CoyoteTime, m_data.MaxDashes);
        }
    }

    public void UpdateFacing(float xInput)
    {
        m_facing.UpdateFacing(xInput);
    }
}