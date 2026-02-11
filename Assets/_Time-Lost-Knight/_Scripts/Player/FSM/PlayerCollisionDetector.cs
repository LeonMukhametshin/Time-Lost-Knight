using UnityEngine;

public class PlayerCollisionDetector : MonoBehaviour
{
    private const float TOLERANCE = 0.015f;

    public int facingDirection { get; set; }

    [SerializeField] private Transform m_groundCheck;
    [SerializeField] private Transform m_wallCheck;
    [SerializeField] private Transform m_ledgeCheck;
    [SerializeField] private Transform m_ceilingCheck;

    private float m_groundCheckRadius;
    private float m_ceilingCheckRadius;
    private float m_wallCheckDistance;
    private LayerMask m_groundLayer;

    private Vector2 m_workspace;

    public void Initialize(float groundCheckRadius, float ceilingCheckRadius, float wallCheckDistance, LayerMask groundLayer)
    {
        m_groundCheckRadius = groundCheckRadius;
        m_ceilingCheckRadius = ceilingCheckRadius;
        m_wallCheckDistance = wallCheckDistance;
        m_groundLayer = groundLayer;

        facingDirection = 1;
    }

    public bool CheckGrounded() =>
        Physics2D.OverlapCircle(m_groundCheck.position, m_groundCheckRadius, m_groundLayer);

    public bool CheckWallTouch() =>
        Physics2D.Raycast(m_wallCheck.position, Vector2.right * facingDirection, m_wallCheckDistance, m_groundLayer);

    public bool CheckWallTouchBask() =>
        Physics2D.Raycast(m_wallCheck.position, Vector2.right * -facingDirection, m_wallCheckDistance, m_groundLayer);

    public bool CheckCeilingCheck() =>
        Physics2D.OverlapCircle(m_ceilingCheck.position, m_ceilingCheckRadius, m_groundLayer);

    public bool CheckTouchingLedge() =>
        Physics2D.Raycast(m_ledgeCheck.position, Vector2.right * facingDirection, m_wallCheckDistance, m_groundLayer);

    public Vector2 DetermineCornerPosition()
    {
        RaycastHit2D xHit = Physics2D.Raycast(m_wallCheck.position, Vector2.right * facingDirection,
            m_wallCheckDistance, m_groundLayer);
        RaycastHit2D yHit = Physics2D.Raycast(m_ledgeCheck.position + (Vector3)(m_workspace), Vector2.down,
           m_ledgeCheck.position.y - m_wallCheck.position.y + TOLERANCE, m_groundLayer);

        float xDistance = xHit.distance;
        float yDistance = yHit.distance;

        m_workspace.Set((xDistance + TOLERANCE) * facingDirection, 0f);
        m_workspace.Set(m_wallCheck.position.x + (xDistance * facingDirection), m_ledgeCheck.position.y - yDistance);

        return m_workspace;
    }
}