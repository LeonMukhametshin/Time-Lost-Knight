using UnityEngine;

[CreateAssetMenu(fileName = "EnemyFirst", menuName = "Scriptable Objects/Enemy/EnemyFirst")]
public sealed class EnemyFirstData : EntityData
{
    [field: SerializeField] public IdleStateData idle { get; private set; }
    [field: SerializeField] public MoveStateData move { get; private set; }
    [field: SerializeField] public PlayerDetectedData playerDetected { get; private set; }
    [field: SerializeField] public ChargeStateData chargeState { get; private set; }
    [field: SerializeField] public LookForPlayerStateData lookForPlayer { get; private set; }
    [field: SerializeField] public MeleeAttackStateData meleeAttackState { get; private set; }
    [field: SerializeField] public StunStateData stanState { get; private set; }
}