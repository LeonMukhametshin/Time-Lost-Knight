using Game.Enemies.States;
using Game.Enemies.States.Datas;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Enemies.Impls.Enemy1
{
    [CreateAssetMenu(fileName = "EnemyFirst", menuName = "Scriptable Objects/Enemy/EnemyFirst")]
    [MovedFrom("")]
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
}

