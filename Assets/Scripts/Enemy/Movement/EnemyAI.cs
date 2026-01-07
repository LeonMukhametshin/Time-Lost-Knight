using UnityEngine;

public class EnemyAI 
{
    private Transform m_transform;
    private Transform[] m_points;
    private float m_moveSpeed;

    private int m_nextWaypoint = 1;
    private float m_distanceToPoint;
  
    public EnemyAI(Transform transform, Transform[] points, float moveSpeed )
    {
        m_transform = transform;
        m_points = points;
        m_moveSpeed = moveSpeed;
    }

    public void Move()
    {
        m_distanceToPoint = Vector2.Distance(m_transform.position, m_points[m_nextWaypoint].position);

        m_transform.position = Vector2.MoveTowards(m_transform.position, m_points[m_nextWaypoint].position,
            m_moveSpeed * Time.deltaTime);

        if(m_distanceToPoint < 0.2f)
        {
            TakeTurn();
        }
    }

    private void TakeTurn()
    { 
        SetRotationFromWaypoint();
        ChooseNextWaypoint();
        FaceNextWaypoint();
    }

    private void FaceNextWaypoint()
    {
        Vector3 toTarget = m_points[m_nextWaypoint].position - m_transform.position;

        toTarget = m_transform.InverseTransformDirection(toTarget);

        float scaleSign = Mathf.Sign(toTarget.x);

        if (Mathf.Abs(toTarget.x) < 0.01f)
        {
            scaleSign = Mathf.Sign(m_transform.localScale.x);
        }

        Vector3 scale = m_transform.localScale;
        scale.x = Mathf.Abs(scale.x) * scaleSign;
        m_transform.localScale = scale;
    }

    private void SetRotationFromWaypoint()
    {
        Vector3 currentRotation = m_transform.eulerAngles;
        currentRotation.z = m_points[m_nextWaypoint].eulerAngles.z;
        m_transform.eulerAngles = currentRotation;
    }

    private void ChooseNextWaypoint()
    {
        m_nextWaypoint++;
        
        if(m_nextWaypoint >= m_points.Length)
        {
            m_nextWaypoint = 0;
        }
    }
}