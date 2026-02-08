using UnityEngine;

public class EnemyModel : MonoBehaviour
{
    [SerializeField] private HealthSystem m_healthSystem;

    private IEnemyBehaviour m_enemyBehaviuor;

    private EnemyData m_enemyData;
    [SerializeField] private EnemyCollision m_enemyCollision;

    public void Initialize(EnemyData data)
    {
        m_enemyData = data;

        switch (data.enemyBehaviuorData)
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

        m_healthSystem.died += () => Destroy(gameObject);

        m_enemyCollision.touchEnemy += Attack;
    }

    private void Update()
    {
        m_enemyBehaviuor?.Move();
    }

    private void Attack()
    {

    }
}   