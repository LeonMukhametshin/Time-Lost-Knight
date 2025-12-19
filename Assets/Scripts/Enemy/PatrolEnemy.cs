using UnityEngine;

public class PatrolEnemy : IMovement
{
    private BaseEnemy m_baseEnemy;
    private Transform m_currentTarget;
    private Transform m_enemyTransform;
    private float m_reachedDistance = 0.5f;
    private int m_indexPoint;
    private Transform[] m_transformPoints;

    public PatrolEnemy(BaseEnemy baseEnemy)
    {
        m_baseEnemy = baseEnemy;
        m_transformPoints = m_baseEnemy.transformPoints;
        m_enemyTransform = m_baseEnemy.m_transform;
        m_currentTarget = m_baseEnemy.transformPoints[0];
    }

    public void Move(Vector2 direction)
    {
        if (m_baseEnemy.m_groundChecker.IsPathBlocked(direction))
        {
            SwitchTarget();
            Debug.Log("Blocked");
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
        m_baseEnemy.Rigidbody2D.linearVelocity = Vector2.zero;
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
        if (m_transformPoints == null || m_transformPoints.Length == 0)
        {
            Debug.LogWarning("Patrol points null");
            return;
        }

        m_indexPoint++;

        if(m_transformPoints.Length <= m_indexPoint)
        {
            m_indexPoint = 0;
        }

        m_currentTarget = m_transformPoints[m_indexPoint];

        if ((m_enemyTransform.localScale.x * UpdateDirection().x) > 0)
        {
            Flip(); 
        }
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
