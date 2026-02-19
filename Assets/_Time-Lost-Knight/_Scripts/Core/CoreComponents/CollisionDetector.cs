using UnityEngine;

public class CollisionDetector : CoreComponent
{
    private const float TOLERANCE = 0.015f;

    private FlipContoller m_flipContoller;
    public FlipContoller flipController
    {
        get => m_flipContoller ??= core.GetCoreComponent<FlipContoller>();
    }

    [SerializeField] private Transform m_groundCheck;
    [SerializeField] private Transform m_wallCheck;
    [SerializeField] private Transform m_ledgeCheck;
    [SerializeField] private Transform m_ceilingCheck;

    [SerializeField] private float m_standColliderHeight;

    [SerializeField] private float m_groundCheckRadius;
    [SerializeField] private float m_ceilingCheckRadius;
    [SerializeField] private float m_wallCheckDistance;

    [SerializeField] private LayerMask m_groundLayer;
    [SerializeField] private LayerMask m_platform;

    private Vector2 m_workspace;

    public override void Awake()
    {
        base.Awake();
    }

    public bool CheckGrounded() =>
        Physics2D.OverlapCircle(m_groundCheck.position, m_groundCheckRadius, m_groundLayer) ||
        Physics2D.OverlapCircle(m_groundCheck.position, m_groundCheckRadius, m_platform);

    public bool CheckCeilingCheck() =>
        Physics2D.OverlapCircle(m_ceilingCheck.position, m_ceilingCheckRadius, m_groundLayer);

    public bool CheckWallTouch() =>
        Physics2D.Raycast(m_wallCheck.position, Vector2.right * flipController.facingDirection, 
            m_wallCheckDistance, m_groundLayer);

    public bool CheckWallTouchBask() =>
        Physics2D.Raycast(m_wallCheck.position, Vector2.right * -flipController.facingDirection, 
            m_wallCheckDistance, m_groundLayer);

    public bool CheckTouchingLedge() =>
        Physics2D.Raycast(m_ledgeCheck.position, Vector2.right * flipController.facingDirection,
            m_wallCheckDistance, m_groundLayer);

    public bool CheckTouckingPlatform() => 
        Physics2D.OverlapCircle(m_groundCheck.position, m_groundCheckRadius, m_platform);

    public bool CheckForSpace(Vector2 cornerPosition) =>
        Physics2D.Raycast(cornerPosition + (Vector2.up * TOLERANCE) + 
            (Vector2.right * flipController.facingDirection * TOLERANCE),
            Vector2.up, m_standColliderHeight, m_groundLayer);

    public Vector2 DetermineCornerPosition()
    {
        RaycastHit2D xHit = Physics2D.Raycast(m_wallCheck.position, Vector2.right * flipController.facingDirection,
            m_wallCheckDistance, m_groundLayer);
        RaycastHit2D yHit = Physics2D.Raycast(m_ledgeCheck.position + (Vector3)(m_workspace), Vector2.down,
           m_ledgeCheck.position.y - m_wallCheck.position.y + TOLERANCE, m_groundLayer);

        float xDistance = xHit.distance;
        float yDistance = yHit.distance;

        m_workspace.Set((xDistance + TOLERANCE) * flipController.facingDirection, 0f);
        m_workspace.Set(m_wallCheck.position.x + (xDistance * flipController.facingDirection), m_ledgeCheck.position.y - yDistance);

        return m_workspace;
    }
}