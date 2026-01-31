using UnityEngine;

public class EnemyViewModel : MonoBehaviour
{
    [SerializeField] private EnemyData m_enemyData;
    [SerializeField] private HealthSystem m_healthSystem;
    [SerializeField] private EnemyAttackSystem m_enemyAttackSystem;

    private IEnemyBehaviour m_enemyBehaviuor;

    public void Initialize()
    {
        // TODO Factory
        // too dirty
        switch(m_enemyData.enemyBehaviuorData)
        {
            case PatrolBehaviourData patrol:
                m_enemyBehaviuor = 
                    gameObject.GetComponent<IEnemyBehaviour>() ??
                    gameObject.AddComponent<EnemyPatrolBehaviour>();
                break;

            case WaypontsBehaviourData wayponts:
                m_enemyBehaviuor =
                   gameObject.GetComponent<IEnemyBehaviour>() ??
                   gameObject.AddComponent<EnemyWaypointBehaviour>();
                break;
        }

        m_enemyBehaviuor.Initialize(m_enemyData.enemyBehaviuorData);
        m_enemyAttackSystem.Initialize(m_enemyData.weaponConfig, m_enemyData.weaponConfig.cooldown);

        m_healthSystem.died += () => Destroy(gameObject);
    }

    private void Update()
    {
        m_enemyBehaviuor?.Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<HealthSystem>(out var healt))
        {
            m_enemyAttackSystem.TryAttack();
        }
    }
}   