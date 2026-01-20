using UnityEngine;

public class EnemyViewModel : MonoBehaviour
{
    [SerializeField] private EnemyData m_enemyData;
    [SerializeField] private HealthSystem m_healthSystem;

    private IEnemyBehaviour m_enemyBehaviuor;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
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

        m_healthSystem.died += () => Destroy(gameObject);
    }

    private void Update()
    {
        m_enemyBehaviuor.Move();
    }
}   