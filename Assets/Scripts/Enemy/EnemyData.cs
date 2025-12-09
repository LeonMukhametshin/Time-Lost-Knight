using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public sealed class EnemyData : ScriptableObject
{
    [field: SerializeField] public int m_maxHealt { get; }
    [field: SerializeField] public int m_initialHealth { get; }
}
