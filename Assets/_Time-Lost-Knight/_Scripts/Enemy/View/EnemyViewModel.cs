using UnityEngine;

public class EnemyViewModel : MonoBehaviour
{
    [SerializeField] private EnemyData m_enemyData;
    [SerializeField] private Transform m_contactChecker;
    [SerializeField] private Transform[] m_waypoints;

    private IDamageable m_damageable;
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

        m_damageable = enemy as IDamageable;
        m_enemyBehaviuor = enemy as IEnemyBehaviuor;
        m_attack = enemy as IAttack;
    }

    private void Update()
    {
        m_enemyBehaviuor?.Move();
    }

    public void TakeDamage(int amout) =>
        m_damageable?.TakeDamage(amout);
}   