using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Enemy Parametrs")]
    [SerializeField] private EnemyData m_enemyData;
    [SerializeField] private BaseEnemy m_enemyController;
    [SerializeField] private EnemyState m_currentState; 

    [Header("Patrol Points")]
    [SerializeField] private Transform m_enemyTransform;
    [SerializeField] private float m_findingPathTime;
    [SerializeField] private float m_distanceWalked;
    [SerializeField] private int m_directionSign;
    [SerializeField] private float m_currentRoamRadius;
    [SerializeField] private Vector3 m_startWalkPosition;

    [SerializeField] private Collider2D m_collider;
    [SerializeField] private Rigidbody2D m_rigidbody2D;

    [Header("Ground Check")]
    [SerializeField] private GroundCheck m_groundChecker;
    public enum EnemyState
    {
        FindingPath,
        WalkToTarget,
        Dead
    }
    private void Awake()
    {
        InitializeComponents();
    }
    private void Update()
    {
        switch (m_currentState)
        {
            case EnemyState.FindingPath:
                UpdateFindingPathState(0);
                break;
            case EnemyState.WalkToTarget:
                WalkToTargetState();
                break;
        }
    }
    private void InitializeComponents()
    {
        if (m_rigidbody2D == null) m_rigidbody2D = GetComponent<Rigidbody2D>();
        if (m_collider == null) m_collider = GetComponent<Collider2D>();

        m_currentState = EnemyState.FindingPath;
        StartFindingPath();
    }
    private void StartFindingPath()
    {
        m_currentState = EnemyState.FindingPath;
        m_findingPathTime = Random.Range(
            m_enemyData.m_findingPathTimeMin,
            m_enemyData.m_findingPathTimeMax
        );
        m_distanceWalked = 0f;
    }
    private void UpdateFindingPathState(int _direction)
    {
         m_findingPathTime -= Time.deltaTime;

        if (m_findingPathTime <= 0)
        {
            if (_direction == 0)
            {
                m_directionSign = Random.Range(0, 2) == 0 ? -1 : 1;
            }
            else { m_directionSign = m_directionSign * -1; }

            m_currentRoamRadius = Random.Range(
                m_enemyData.m_roamRadiusMin,
                m_enemyData.m_roamRadiusMax
            );

            m_currentState = EnemyState.WalkToTarget;
            m_startWalkPosition = transform.position;
            m_distanceWalked = 0f;
        }
    }
    private void WalkToTargetState()
    {
        if (m_groundChecker.IsPathBlocked(m_directionSign))
        {
            UpdateFindingPathState(m_directionSign);
        }

        m_distanceWalked = Mathf.Abs(transform.position.x - m_startWalkPosition.x);

        if (m_distanceWalked >= m_currentRoamRadius)
        {
            StartFindingPath();
            return;
        }
        MoveInDirection();
    }
    private void MoveInDirection()
    {
        m_enemyTransform.position = new Vector3(m_enemyTransform.position.x + Time.deltaTime * m_directionSign * m_enemyData.m_moveSpeed,
            m_enemyTransform.position.y, m_enemyTransform.position.z);
    }

}
