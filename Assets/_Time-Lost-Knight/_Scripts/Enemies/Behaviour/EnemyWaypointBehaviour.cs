using UnityEngine;

public class EnemyWaypointBehaviour : MonoBehaviour, IEnemyBehaviour
{
    [SerializeField] private GameObject[] m_points;

    private Transform m_transform;
    private float m_moveSpeed;

    private int m_nextWaypoint = 0;
    private float m_distanceToPoint;

    private bool m_isIntialized;

    public void Initialize(EnemyBehaviuorData data)
    {
        m_transform = transform;
        m_moveSpeed = data.moveSpeed;

        m_isIntialized = true;
    }

    public void Move()
    {
        if(!m_isIntialized)
        {
            return;
        }

        m_distanceToPoint = Vector2.Distance(
            m_transform.position, m_points[m_nextWaypoint].transform.position);

        m_transform.position = Vector2.MoveTowards(
            m_transform.position, 
            m_points[m_nextWaypoint].transform.position,
            m_moveSpeed * Time.deltaTime);

        if(m_distanceToPoint < 0.05f)
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
        Vector3 direction = m_points[m_nextWaypoint].transform.position - m_transform.position;
        direction = m_transform.InverseTransformDirection(direction);
        float scaleSign = Mathf.Sign(direction.x);

        if (Mathf.Abs(direction.x) < 0.01f)
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
        currentRotation.z = m_points[m_nextWaypoint].transform.eulerAngles.z;
        m_transform.eulerAngles = currentRotation;
    }

    private void ChooseNextWaypoint()
    {
        m_nextWaypoint++;

        if (m_nextWaypoint >= m_points.Length)
        {
            m_nextWaypoint = 0;
        }
    }
}