using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData m_enemyData;
    [SerializeField] private HealthSystem m_healt;

    [SerializeField] private Transform[] m_wayPoints;

    private EnemyAI m_movement;

    private void Awake()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        m_healt = new HealthSystem(m_enemyData.maxHealt);
        m_movement = new EnemyAI(transform, m_wayPoints, m_enemyData.moveSpeed);
    }

    private void Update()
    {
        if(m_movement is null)
        {
            return;
        }

        m_movement.Move();
    }
}