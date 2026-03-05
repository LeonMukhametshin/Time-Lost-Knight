using UnityEngine;

[CreateAssetMenu(fileName = "EnemyFour", menuName = "Scriptable Objects/Enemy/EnemyFour")]
public sealed class EnemyFourData : EntityData
{
    [field: SerializeField] public IdleStateData idle { get; private set; }
    [field: SerializeField] public MoveStateData move { get; private set; }
    [field: SerializeField] public LookForPlayerStateData lookForPlayer { get; private set; }
    [field: SerializeField] public PlayerDetectedData playerDetected { get; private set; }
    [field: SerializeField] public RangeAttackData rangeAttack { get; private set; }
}