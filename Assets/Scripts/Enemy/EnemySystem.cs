using UnityEngine;
using static Enemy;

public class EnemySystem : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;

    public HealthSystem m_healt { get; private set; }
    public IMovement m_movement { get; private set; }
    public EnemyState m_currentState { get; private set; }

    private void Awake()
    {
        InitializeComponent();
    }

    private void Update()
    {
        m_movement.Update();
    }

    private void InitializeComponent()
    {
        m_healt = new HealthSystem(enemyData.m_maxHealt, enemyData.m_initialHealth);
        m_healt.Death += Death;
    }
    private void Damaged(HealthSystem health)
    {
        health.TakeDamage(enemyData.m_damage);
    }
    private void Death()
    {
        gameObject.SetActive(false);
    }
}
