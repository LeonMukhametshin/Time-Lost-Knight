using UnityEngine;

[CreateAssetMenu(fileName = "AttackData", menuName = "Scriptable Objects/Enemy/AttackData")]
public class AttackData : ScriptableObject
{
    [field: SerializeField] public int damage { get; private set; }
    [field: SerializeField] public float attackCooldown { get; private set; }
    [field: SerializeField] public float attackRange { get; private set; }
    [field: SerializeField] public float colliderDistanceMultiplier { get; private set; }
}