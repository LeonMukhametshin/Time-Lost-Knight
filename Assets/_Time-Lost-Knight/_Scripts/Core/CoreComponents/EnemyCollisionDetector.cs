using UnityEngine;

public class EnemyCollisionDetector : CollisionDetector
{
    [SerializeField] private Transform m_playerChecker;

    [SerializeField] private LayerMask m_playerLayer;

    [SerializeField][Min(0)] private float m_minAgroDistance = 3f;
    [SerializeField][Min(0)] private float m_maxAgroDistance = 15f;
    [SerializeField][Min(0)] private float m_closeRangeActionDistance = 1f;

    public virtual bool CheckLedge() =>
        Physics2D.Raycast(m_ledgeCheck.position, Vector2.down,
            m_wallCheckDistance, m_groundLayer);

    public virtual bool CheckPlayerInMinAgroRange() =>
        Physics2D.Raycast(m_playerChecker.position, transform.right,
            m_minAgroDistance, m_playerLayer);

    public virtual bool CheckPlayerInMaxAgroRange() =>
        Physics2D.Raycast(m_playerChecker.position, transform.right,
            m_maxAgroDistance, m_playerLayer);


    public virtual bool CheckPlayerInCloseRangeAction() => 
        Physics2D.Raycast(m_playerChecker.position, transform.right, m_closeRangeActionDistance, m_playerLayer);

    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(m_wallCheck.position,
            m_wallCheck.position + (Vector3)(Vector2.right * flipController.facingDirection * m_wallCheckDistance));
        Gizmos.DrawLine(m_ledgeCheck.position,
            m_ledgeCheck.position + (Vector3)(Vector2.down * m_wallCheckDistance));

        Gizmos.DrawWireSphere(m_playerChecker.position + (Vector3)(Vector2.right *  m_closeRangeActionDistance * flipController.facingDirection),
            0.2f);
        Gizmos.DrawWireSphere(m_playerChecker.position + (Vector3)(Vector2.right * m_minAgroDistance * flipController.facingDirection),
            0.2f);
        Gizmos.DrawWireSphere(m_playerChecker.position + (Vector3)(Vector2.right * m_maxAgroDistance * flipController.facingDirection),
            0.2f);
    }
}