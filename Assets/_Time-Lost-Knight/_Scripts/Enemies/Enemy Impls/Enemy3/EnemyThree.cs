using Game.Core.CoreComponents;
using Game.Enemies;
using Game.Enemies.Impls.Enemy1.States;
using Game.Enemies.Impls.Enemy3.States;
using Game.Entities;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Enemies.Impls.Enemy3
{
    [MovedFrom("")]
    public class EnemyThree : Entity
    {
        [SerializeField] private Transform m_rangeAttackPosition;

        public override void Awake()
        {
            base.Awake();
            var enemyThreeData = data as EnemyThreeData;

            fsm.Initialize(
                new EnemyThreeIdleState(fsm, core, EnemyAnimationConst.IDLE, this, enemyThreeData.idle),
                new EnemyThreeLookForPlayerState(fsm, core, EnemyAnimationConst.LOOK_FOR_PLAYER, this, enemyThreeData.lookForPlayer),
                new EnemyThreePlayerDetectedState(fsm, core, EnemyAnimationConst.PLAYER_DETECTED, this, enemyThreeData.playerDetected),
                new EnemyThreeRangeAttackState(fsm, core, EnemyAnimationConst.RANGED_ATTACK, this, m_rangeAttackPosition, enemyThreeData.rangeAttack));

            animationToFSM.Initialize(fsm);

            fsm.ChangeState<EnemyThreeIdleState>();
        }
    }
}

