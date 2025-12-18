using UnityEngine;
using UnityEngine.UIElements;

public class GroundCheck 
{
    private BaseEnemy m_baseEnemy;

    private Transform m_enemyTransform;

    public GroundCheck(BaseEnemy baseEnemy)
    {
        m_baseEnemy = baseEnemy;
        m_enemyTransform = m_baseEnemy.transform;
    }

    public bool HasGroundAhead()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            m_baseEnemy.poitGroundCheck.position,
            Vector2.down,
            m_baseEnemy.enemyData.groundCheckDistance,
            m_baseEnemy.enemyData.groundLayer
        );

        return hit.collider != null;
    }

    public bool HasObstacleAhead(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(
            m_enemyTransform.position,
            direction.normalized,
            m_baseEnemy.enemyData.obstacleCheckDistance,
            m_baseEnemy.enemyData.obstacleLayer
        );

        Debug.DrawRay(m_enemyTransform.position, direction.normalized * m_baseEnemy.enemyData.obstacleCheckDistance, Color.yellow);

        return hit.collider != null;
    }

    public bool IsPathBlocked(Vector2 direction) =>
        HasObstacleAhead(direction) || !HasGroundAhead();

}
