using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData m_enemyData;
    [SerializeField] private HealthSystem m_healt;

    private void Awake()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        m_healt = new HealthSystem(m_enemyData.m_maxHealt, m_enemyData.m_initialHealth);
    }
}
