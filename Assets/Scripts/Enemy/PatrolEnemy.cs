using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    [SerializeField] private Transform m_pointA;
    [SerializeField] private Transform m_pointB;
    [SerializeField] private Rigidbody2D m_rigidbody2D;

    [SerializeField] private float m_speed;

    private Transform m_currentPoint;

    private void OnValidate()
    {
        if (!m_rigidbody2D)
        {
            m_rigidbody2D = GetComponent<Rigidbody2D>();
        }
        if (!m_pointA || !m_pointB)
        {
            Debug.Log("Point A or Point B is not assigned");
        }
    }

    private void Awake()
    {
        m_currentPoint = m_pointB;
    }

    private void Update()
    {
        var point = m_currentPoint.position - transform.position;

        if (m_currentPoint.position == m_pointB.position)
        {
            m_rigidbody2D.linearVelocityX = m_speed;
        }
        else
        {
            m_rigidbody2D.linearVelocityX = -m_speed;
        }

        if (Vector2.Distance(transform.position, m_currentPoint.position) < 0.5f 
            && m_currentPoint == m_pointB)
        {
            Flip();
            m_currentPoint = m_pointA;
        }
        if (Vector2.Distance(transform.position, m_currentPoint.position) < 0.5f
            && m_currentPoint == m_pointA)
        {
            Flip();
            m_currentPoint = m_pointB;
        }
    }

    private void Flip()
    {
        var localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}