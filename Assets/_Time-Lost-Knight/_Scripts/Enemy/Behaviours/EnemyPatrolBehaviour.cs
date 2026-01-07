using UnityEngine;

public class EnemyPatrolBehaviour : IEnemyBehaviuor
{
    private Transform m_contactChecker;
    private float m_moveSpeed;
    private float m_rayLenght;
    private LayerMask m_layerMask = LayerMask.NameToLayer("Ground");

    private Vector2 m_moveDirection = Vector2.left;

    private Transform m_transform;

    public EnemyPatrolBehaviour(Transform transform, Transform contactChecker, float moveSpeed, float rayLenght)
    {
        m_transform = transform;
        m_contactChecker = contactChecker;
        m_moveSpeed = moveSpeed;
        m_rayLenght = rayLenght;
    }

    public void Move()
    {
        if (HasContact(m_moveDirection))
        {
            Flip();
        }
        if (!HasContact(Vector2.down))
        {
            Flip();
        }

        Translate();
    }

    private void Translate()
    {
        m_transform.Translate(m_moveDirection * m_moveSpeed * Time.deltaTime);
    }

    private void Flip()
    {
        m_moveDirection *= -1;

        Vector3 scale = m_transform.localScale;
        scale.x *= -1;
        m_transform.localScale = scale;
    }

    private bool HasContact(Vector2 direction) =>
        Physics2D.Raycast(
            m_contactChecker.position,
            direction,
            m_rayLenght,
            m_layerMask);
}