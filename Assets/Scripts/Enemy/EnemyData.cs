using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public sealed class EnemyData : ScriptableObject
{
    [Header("Health Settings")]
    [field: SerializeField] public int m_maxHealt { get; private set; }
    [field: SerializeField] public int m_initialHealth { get; private set; }

    [Header("Movement Settings")]
    [field: SerializeField] public float m_moveSpeed { get; private set; }
    [field: SerializeField] public float m_roamRadiusMin { get; private set; }
    [field: SerializeField] public float m_roamRadiusMax { get; private set; }
    [field: SerializeField] public float m_findingPathTimeMin { get; private set; }
    [field: SerializeField] public float m_findingPathTimeMax { get; private set; }

    [Header("Ground Check Settings")]
    [field: SerializeField] public float groundCheckDistance { get; private set; } //Длина луча для проверки земли под врагом
    [field: SerializeField] public float obstacleCheckDistance { get; private set; } // Длина луча для проверки препятствий перед врагом
    [field: SerializeField] public LayerMask groundLayer { get; private set; }
    [field: SerializeField] public LayerMask obstacleLayer { get; private set; }

    [Header("Combat Settings")]
    [field: SerializeField] public int m_damage { get; private set; }
    [field: SerializeField] public float m_attackCooldown { get; private set; }
    [field: SerializeField] public float m_attackRange { get; private set; } // 2
    //[field: SerializeField] public float detectionRange { get; private set; }
    [field: SerializeField] public float m_colliderDistanceMultiplier { get; private set; } //-0.25
}
