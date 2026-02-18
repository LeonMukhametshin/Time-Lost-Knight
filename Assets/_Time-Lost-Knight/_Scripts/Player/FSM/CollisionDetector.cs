using System;
using UnityEngine;

[Serializable]
public class CollisionDetector 
{
    private const float TOLERANCE = 0.015f;

    public int facingDirection { get; set; }

    [SerializeField] private CheckTransfomRef m_transformRef;

    private float m_standColliderHeight;

    private float m_groundCheckRadius;
    private float m_ceilingCheckRadius;
    private float m_wallCheckDistance;

    private LayerMask m_groundLayer;
    private LayerMask m_platformLayer;

    private Vector2 m_workspace;

    public CollisionDetector(CheckersData data, CheckTransfomRef transformRef, float playerheight)
    {
        m_groundCheckRadius = data.groundCheckRadius;
        m_ceilingCheckRadius = data.ceilingCheckRadius;
        m_wallCheckDistance = data.wallCheckDistance;

        m_groundLayer = data.groundLayer;
        m_platformLayer = data.platformLayer;

        m_standColliderHeight = playerheight;

        m_transformRef = transformRef;
        facingDirection = 1;    
    }

    public bool CheckGrounded() =>
        Physics2D.OverlapCircle(m_transformRef.groundCheck.position, m_groundCheckRadius, m_groundLayer);

    public bool CheckCeilingCheck() =>
        Physics2D.OverlapCircle(m_transformRef.ceilingCheck.position, m_ceilingCheckRadius, m_groundLayer);

    public bool CheckWallTouch() =>
        Physics2D.Raycast(m_transformRef.wallCheck.position, Vector2.right * facingDirection, 
            m_wallCheckDistance, m_groundLayer);

    public bool CheckWallTouchBask() =>
        Physics2D.Raycast(m_transformRef.wallCheck.position, Vector2.right * -facingDirection, 
            m_wallCheckDistance, m_groundLayer);

    public bool CheckTouchingLedge() =>
        Physics2D.Raycast(m_transformRef.ledgeCheck.position, Vector2.right * facingDirection,
            m_wallCheckDistance, m_groundLayer);

    public bool CheckIsOnPlatform() =>
        Physics2D.Raycast(m_transformRef.groundCheck.position, Vector2.down, m_groundCheckRadius,
            m_platformLayer);

    public bool CheckForSpace(Vector2 cornerPosition) =>
        Physics2D.Raycast(cornerPosition + (Vector2.up * TOLERANCE) + (Vector2.right * facingDirection * TOLERANCE),
            Vector2.up, m_standColliderHeight, m_groundLayer);

    public Vector2 DetermineCornerPosition()
    {
        RaycastHit2D xHit = Physics2D.Raycast(m_transformRef.wallCheck.position, Vector2.right * facingDirection,
            m_wallCheckDistance, m_groundLayer);
        RaycastHit2D yHit = Physics2D.Raycast(m_transformRef.ledgeCheck.position + (Vector3)(m_workspace), Vector2.down,
           m_transformRef.ledgeCheck.position.y - m_transformRef.wallCheck.position.y + TOLERANCE, m_groundLayer);

        float xDistance = xHit.distance;
        float yDistance = yHit.distance;

        m_workspace.Set((xDistance + TOLERANCE) * facingDirection, 0f);
        m_workspace.Set(m_transformRef.wallCheck.position.x + (xDistance * facingDirection), 
            m_transformRef.ledgeCheck.position.y - yDistance);

        return m_workspace;
    }
}