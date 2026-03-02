using UnityEngine;


[CreateAssetMenu(fileName = "MeleeAttackState_Data", menuName = "Scriptable Objects/State Data/MeleeAttackState_Data")]
public class MeleeAttackStateData : ScriptableObject
{
    [field: SerializeField] public float attackRadius { get; private set; }
    [field: SerializeField] public float attackDamage { get; private set; }
    [field: SerializeField] public LayerMask playerMask { get; private set; }

    [field: SerializeField] public Vector2 angle { get; private set; }
    [field: SerializeField] public float knokbackStringth { get; private set; }
}