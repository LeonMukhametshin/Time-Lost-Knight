using UnityEngine;

public class CollisionDetector : CoreComponent
{
    private const float TOLERANCE = 0.015f;

    public FlipContoller flipController { get; private set; }

    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;
    [SerializeField] private Transform ceilingCheck;

    [SerializeField] private float m_standColliderHeight;

    [SerializeField] private float m_groundCheckRadius;
    [SerializeField] private float m_ceilingCheckRadius;
    [SerializeField] private float m_wallCheckDistance;

    [SerializeField] private LayerMask m_groundLayer;

    private Vector2 m_workspace;

    public override void Awake()
    {
        base.Awake();

        flipController = core.GetCoreComponent<FlipContoller>();
    }

    public bool CheckGrounded() =>
        Physics2D.OverlapCircle(groundCheck.position, m_groundCheckRadius, m_groundLayer);

    public bool CheckCeilingCheck() =>
        Physics2D.OverlapCircle(ceilingCheck.position, m_ceilingCheckRadius, m_groundLayer);

    public bool CheckWallTouch() =>
        Physics2D.Raycast(wallCheck.position, Vector2.right * flipController.facingDirection, 
            m_wallCheckDistance, m_groundLayer);

    public bool CheckWallTouchBask() =>
        Physics2D.Raycast(wallCheck.position, Vector2.right * -flipController.facingDirection, 
            m_wallCheckDistance, m_groundLayer);

    public bool CheckTouchingLedge() =>
        Physics2D.Raycast(ledgeCheck.position, Vector2.right * flipController.facingDirection,
            m_wallCheckDistance, m_groundLayer);

    public bool CheckForSpace(Vector2 cornerPosition) =>
        Physics2D.Raycast(cornerPosition + (Vector2.up * TOLERANCE) + 
            (Vector2.right * flipController.facingDirection * TOLERANCE),
            Vector2.up, m_standColliderHeight, m_groundLayer);

    public Vector2 DetermineCornerPosition()
    {
        RaycastHit2D xHit = Physics2D.Raycast(wallCheck.position, Vector2.right * flipController.facingDirection,
            m_wallCheckDistance, m_groundLayer);
        RaycastHit2D yHit = Physics2D.Raycast(ledgeCheck.position + (Vector3)(m_workspace), Vector2.down,
           ledgeCheck.position.y - wallCheck.position.y + TOLERANCE, m_groundLayer);

        float xDistance = xHit.distance;
        float yDistance = yHit.distance;

        m_workspace.Set((xDistance + TOLERANCE) * flipController.facingDirection, 0f);
        m_workspace.Set(wallCheck.position.x + (xDistance * flipController.facingDirection), ledgeCheck.position.y - yDistance);

        return m_workspace;
    }
}