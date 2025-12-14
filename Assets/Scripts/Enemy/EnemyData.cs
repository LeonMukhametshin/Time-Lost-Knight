using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public sealed class EnemyData : ScriptableObject
{
    [field: SerializeField] public int m_maxHealt { get; private set; }
    [field: SerializeField] public int m_initialHealth { get; private set; }

    [field: SerializeField] public int m_damage { get; private set; }
    [field: SerializeField] public float m_moveSpeed { get; private set; }
    [field: SerializeField] public float m_attackCooldown { get; private set; }
    [field: SerializeField] public float m_roamRadiusMin { get; private set; }
    [field: SerializeField] public float m_roamRadiusMax { get; private set; }
    [field: SerializeField] public float m_findingPathTimeMin { get; private set; }
    [field: SerializeField] public float m_findingPathTimeMax { get; private set; }
}
