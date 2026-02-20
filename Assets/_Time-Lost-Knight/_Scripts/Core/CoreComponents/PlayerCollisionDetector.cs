using UnityEngine;

public class PlayerCollisionDetector : CollisionDetector
{
    [SerializeField] private Transform m_ceilingCheck;

    [SerializeField][Min(0)] protected float m_standColliderHeight = 1.6f;
    [SerializeField][Min(0)] protected float m_ceilingCheckRadius = 0.3f;

    public bool CheckCeilingCheck() =>
        Physics2D.OverlapCircle(m_ceilingCheck.position, m_ceilingCheckRadius, m_groundLayer);

    public bool CheckWallTouchBack() =>
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