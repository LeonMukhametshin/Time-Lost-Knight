using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public sealed class EnemyData : ScriptableObject
{
    [Header("Health Settings")]
    [field: SerializeField] public int maxHealt { get; private set; }

    [Header("Movement Settings")]
    [field: SerializeField] public float moveSpeed { get; private set; }
   
    [Header("Combat Settings")]
    [field: SerializeField] public int damage { get; private set; }
    [field: SerializeField] public float attackCooldown { get; private set; }
    [field: SerializeField] public float attackRange { get; private set; } 
    [field: SerializeField] public float colliderDistanceMultiplier { get; private set; } 
}