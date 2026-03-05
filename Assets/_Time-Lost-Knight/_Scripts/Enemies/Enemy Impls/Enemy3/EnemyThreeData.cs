using UnityEngine;

[CreateAssetMenu(fileName = "EnemyThree", menuName = "Scriptable Objects/Enemy/EnemyThree")]
public sealed class EnemyThreeData : EntityData
{
    [field: SerializeField] public IdleStateData idle { get; private set; }
    [field: SerializeField] public LookForPlayerStateData lookForPlayer { get; private set; }
    [field: SerializeField] public PlayerDetectedData playerDetected { get; private set; }
    [field: SerializeField] public RangeAttackData rangeAttack { get; private set; }
}