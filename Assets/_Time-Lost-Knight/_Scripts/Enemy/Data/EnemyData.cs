using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public sealed class EnemyData : ScriptableObject
{
    [field: SerializeField] public EnemyType enemyType { get; private set; }

    [Header("Health Settings")]
    [field: SerializeField] public int maxHealt { get; private set; }

    [Header("Movement Settings")]
    [field: SerializeField] public EnemyMovementType movementType { get; private set; }
    [field: SerializeField] public float moveSpeed { get; private set; }

    [Header("Ground Patrol")]
    [field: SerializeField] public float rayLenght { get; private set; }
}