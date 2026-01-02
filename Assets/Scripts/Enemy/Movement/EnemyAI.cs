using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float m_moveSpeed;
    [SerializeField] private Transform[] m_points;

    private int m_nextWaypoint = 1;
    private float m_distanceToPoint;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        m_distanceToPoint = Vector2.Distance(transform.position, m_points[m_nextWaypoint].position);

        transform.position = Vector2.MoveTowards(transform.position, m_points[m_nextWaypoint].position,
            m_moveSpeed * Time.deltaTime);

        if(m_distanceToPoint < 0.2f)
        {
            TakeTurn();
        }
    }

    private void TakeTurn()
    {
        Vector3 currentRotation = transform.eulerAngles;
        currentRotation.z = m_points[m_nextWaypoint].eulerAngles.z;
        Debug.Log(m_nextWaypoint + "    " + m_points[m_nextWaypoint].eulerAngles.z);
        transform.eulerAngles = currentRotation;

        ChooseNextWaypoint();
    }

    private void ChooseNextWaypoint()
    {
        m_nextWaypoint++;
        
        if(m_nextWaypoint >=  m_points.Length)
        {
            m_nextWaypoint = 0;
        }
    }
}
