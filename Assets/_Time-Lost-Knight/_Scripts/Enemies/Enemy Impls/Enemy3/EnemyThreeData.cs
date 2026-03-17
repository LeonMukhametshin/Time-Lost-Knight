using Game.Enemies.States.Datas;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Enemies.Impls.Enemy3
{
    [CreateAssetMenu(fileName = "EnemyThree", menuName = "Scriptable Objects/Enemy/EnemyThree")]
    [MovedFrom("")]
    public sealed class EnemyThreeData : EntityData
    {
        [field: SerializeField] public IdleStateData idle { get; private set; }
        [field: SerializeField] public LookForPlayerStateData lookForPlayer { get; private set; }
        [field: SerializeField] public PlayerDetectedData playerDetected { get; private set; }
        [field: SerializeField] public RangeAttackData rangeAttack { get; private set; }
    }
}

