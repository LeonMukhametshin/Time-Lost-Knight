using UnityEngine;

public abstract class CollisionDetector : CoreComponent
{
    protected const float TOLERANCE = 0.015f;

    private FlipContoller m_flipContoller;
    protected FlipContoller flipController
    {
        get => m_flipContoller ??= core.GetCoreComponent<FlipContoller>();
    }

    [SerializeField] protected Transform m_groundCheck;
    [SerializeField] protected Transform m_wallCheck;
    [SerializeField] protected Transform m_ledgeCheck;

    [SerializeField][Min(0)] protected float m_groundCheckRadius;
    [SerializeField][Min(0)] protected float m_wallCheckDistance;

    [SerializeField] protected LayerMask m_groundLayer;
    [SerializeField] protected LayerMask m_platform;

    protected Vector2 m_workspace;

    public bool CheckGrounded() =>
        Physics2D.OverlapCircle(m_groundCheck.position, m_groundCheckRadius, m_groundLayer) ||
        Physics2D.OverlapCircle(m_groundCheck.position, m_groundCheckRadius, m_platform);

    public bool CheckWallTouch() =>
        Physics2D.Raycast(m_wallCheck.position, Vector2.right * flipController.facingDirection,
            m_wallCheckDistance, m_groundLayer);
}