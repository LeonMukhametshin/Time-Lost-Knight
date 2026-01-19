using UnityEngine;

public class EnemyViewModel : MonoBehaviour
{
    [SerializeField] private EnemyData m_enemyData;

    [SerializeField] private Transform m_contactChecker;
    [SerializeField] private Transform[] m_waypoints;

    [SerializeField] private HealthSystem m_healthSystem;

    private IEnemyBehaviuor m_enemyBehaviuor;
    private IAttack m_attack;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        
    }

    private void Update()
    {
        m_enemyBehaviuor?.Move();
    }

    public void TakeDamage(int amout) =>
        m_healthSystem?.TakeDamage(amout);
}   