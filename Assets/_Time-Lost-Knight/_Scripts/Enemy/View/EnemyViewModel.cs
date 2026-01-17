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
        var enemy = EnemyFactory.Create(
            m_enemyData,
            transform,
            m_contactChecker,
            m_waypoints);

        //m_healthSystem.Initialize(m_enemyData.maxHealt);
        m_enemyBehaviuor = enemy as IEnemyBehaviuor;
        m_attack = enemy as IAttack;
    }

    private void Update()
    {
        m_enemyBehaviuor?.Move();
    }

    public void TakeDamage(int amout) =>
        m_healthSystem?.TakeDamage(amout);
}   