using UnityEngine;

public class EnemyViewModel : MonoBehaviour
{
    [SerializeField] private EnemyData m_enemyData;
    [SerializeField] private HealthSystem m_healthSystem;

    [SerializeField] private Transform m_contactChecker;

    private IEnemyBehaviuor m_enemyBehaviuor;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        m_enemyBehaviuor = new EnemyPatrolBehaviour(transform, m_contactChecker, m_enemyData);
        m_healthSystem.died += () => Destroy(gameObject);
    }

    private void Update()
    {
        m_enemyBehaviuor.Move();
    }
}   