using UnityEngine;

public class PatrolEnemy : IMovement
{
    private BaseEnemy m_baseEnemy;
    private Transform m_currentTarget;
    private Transform m_enemyTransform;
    private bool m_isMoving;
    private float m_reachedDistance = 0.5f; 

    public bool isMoving => m_isMoving;

    public PatrolEnemy(BaseEnemy baseEnemy)
    {
        m_baseEnemy = baseEnemy;
        m_currentTarget = m_baseEnemy.PointA;
        m_enemyTransform = m_baseEnemy.m_transform;
    }

    public void Move(Vector2 direction)
    {
        if (m_baseEnemy.m_groundChecker.IsPathBlocked(direction))
        {
            SwitchTarget();
        }
        if (m_baseEnemy == null || m_baseEnemy.Rigidbody2D == null || m_currentTarget == null)
        {
            return;
        }

        Vector2 velocity = direction * m_baseEnemy.enemyData.m_moveSpeed;

        m_baseEnemy.Rigidbody2D.linearVelocity = new Vector2(velocity.x, m_baseEnemy.Rigidbody2D.linearVelocity.y);
    }

    public void Stop()
    {
        m_isMoving = false;
    }

    private Vector2 UpdateDirection()
    {
        Vector2 direction = (m_currentTarget.position - m_enemyTransform.position).normalized;
        return direction;
    }

    private void CheckIfReachedTarget()
    {
        if (m_currentTarget == null) return;

        if (Vector2.Distance(m_enemyTransform.position, m_currentTarget.position) <= m_reachedDistance)
        { 
            SwitchTarget();
        }
    }

    private void SwitchTarget()
    {
        if (m_currentTarget == m_baseEnemy.PointA)
        {
            m_currentTarget = m_baseEnemy.PointB;
        }
        else
        {
            m_currentTarget = m_baseEnemy.PointA;
        }
        Flip();
        UpdateDirection();
    }

    private void Flip()
    {
        Vector3 localScale = m_enemyTransform.localScale;
        localScale.x *= -1;
        m_enemyTransform.localScale = localScale;
    }

    public void Update() 
    {
        CheckIfReachedTarget();
        Move(UpdateDirection());
    }
}
