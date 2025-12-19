using UnityEngine;

public class GroundCheck 
{
    private GroundCheckData m_groundCheckData;
    private Transform m_groundCheckPoint;
    private Transform m_enemyTransform;

    public GroundCheck(GroundCheckData groundCheckData, Transform transform, Transform groundCheckPoint)
    {
        m_groundCheckData = groundCheckData;
        m_enemyTransform = transform;
        m_groundCheckPoint = groundCheckPoint;
    }

    public bool HasGroundAhead()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            m_groundCheckPoint.position,
            Vector2.down,
            m_groundCheckData.groundCheckDistance,
            m_groundCheckData.groundLayer
        );

        return hit.collider != null;
    }

    public bool HasObstacleAhead(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(
            m_enemyTransform.position,
            direction.normalized,
            m_groundCheckData.obstacleCheckDistance,
            m_groundCheckData.obstacleLayer
        );

        Debug.DrawRay(m_enemyTransform.position, direction.normalized * m_groundCheckData.obstacleCheckDistance, Color.yellow);

        return hit.collider != null;
    }

    public bool IsPathBlocked(Vector2 direction) =>
        HasObstacleAhead(direction) || !HasGroundAhead();
}