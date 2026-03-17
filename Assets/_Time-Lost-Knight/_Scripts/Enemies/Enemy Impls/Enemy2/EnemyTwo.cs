using Game.Core.CoreComponents;
using Game.Enemies;
using Game.Enemies.Impls.Enemy1.States;
using Game.Enemies.Impls.Enemy2.States;
using Game.Enemies.States;
using Game.Entities;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Enemies.Impls.Enemy2
{
    [MovedFrom("")]
    public class EnemyTwo : Entity
    {
        [SerializeField] private Transform m_meleeAttackPosition;
        [SerializeField] private Transform m_rangeAttackPosition;

        public override void Awake()
        {
            base.Awake();

            var enemyTwoData = data as EnemyTwoData;

            fsm.Initialize(
                new EnemyTwoIdleState(fsm, core, EnemyAnimationConst.IDLE, this, enemyTwoData.idle),
                new EnemyTwoMoveState(fsm, core, EnemyAnimationConst.MOVE, this, enemyTwoData.move),
                new EnemyTwoPlayerDetectedState(fsm, core, EnemyAnimationConst.PLAYER_DETECTED, this, enemyTwoData.playerDetected),
                new EnemyTwoDodgeState(fsm, core, EnemyAnimationConst.DODGE, this, enemyTwoData.dodgeState),
                new EnemyTwoRangeAttackState(fsm, core, EnemyAnimationConst.RANGED_ATTACK, this, m_rangeAttackPosition, enemyTwoData.rangeAttack),
                new EnemyTwoMeleeAttackState(fsm, core, EnemyAnimationConst.MELEE_ATTACK, this, m_meleeAttackPosition, enemyTwoData.meleeAttackState),
                new EnemyTwoLookForPlayerState(fsm, core, EnemyAnimationConst.LOOK_FOR_PLAYER, this, enemyTwoData.lookForPlayer),
                new EnemyTwoStunState(fsm, core, EnemyAnimationConst.STUN, this, enemyTwoData.stanState));

            animationToFSM.Initialize(fsm);

            fsm.ChangeState<EnemyTwoIdleState>();
        }
    }
}

