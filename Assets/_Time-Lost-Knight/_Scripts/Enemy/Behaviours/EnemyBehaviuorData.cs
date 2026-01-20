using UnityEngine;

public abstract class EnemyBehaviuorData : ScriptableObject
{
    [Header("Movement Settings")]
    [field: SerializeField] public float moveSpeed { get; private set; }
}