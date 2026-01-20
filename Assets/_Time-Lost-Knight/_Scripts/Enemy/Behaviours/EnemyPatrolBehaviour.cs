using UnityEngine;

public class EnemyPatrolBehaviour : MonoBehaviour, IEnemyBehaviour
{
    [SerializeField] private Transform m_contactChecker;
    private Transform m_transform;

    private float m_moveSpeed;
    private float m_rayLenght;

    private Vector2 m_moveDirection = Vector2.right;

    private bool m_isInitilized;

    public void Initialize(EnemyBehaviuorData data)
    {
        m_transform = transform;
        m_moveSpeed = data.moveSpeed;

        if(data is PatrolBehaviourData patrolData)
        {
            m_rayLenght = patrolData.rayLenght;
        } 

        m_isInitilized = true;
    }

    public void Initialize(PatrolBehaviourData data)
    {
       
    }

    public void Move()
    {
        if (!m_isInitilized)
        {
            return;
        }

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

    private void Translate() =>
         m_transform.Translate(m_moveDirection * m_moveSpeed * Time.deltaTime);

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
            m_rayLenght);
}