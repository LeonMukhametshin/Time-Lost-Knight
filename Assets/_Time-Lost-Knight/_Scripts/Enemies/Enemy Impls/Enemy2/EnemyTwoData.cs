using UnityEngine;

[CreateAssetMenu(fileName = "EnemyTwo", menuName = "Scriptable Objects/Enemy/EnemyTwo")]
public sealed class EnemyTwoData : EntityData
{
    [field: SerializeField] public IdleStateData idle { get; private set; }
    [field: SerializeField] public MoveStateData move { get; private set; }
    [field: SerializeField] public PlayerDetectedData playerDetected { get; private set; }
    [field: SerializeField] public MeleeAttackStateData meleeAttackState { get; private set; }
    [field: SerializeField] public LookForPlayerStateData lookForPlayer { get; private set; }
    [field: SerializeField] public StunStateData stanState { get; private set; }
    [field: SerializeField] public RangeAttackData rangeAttack { get; private set; }
    [field: SerializeField] public DodgeStateData dodgeState { get; private set; }
}
