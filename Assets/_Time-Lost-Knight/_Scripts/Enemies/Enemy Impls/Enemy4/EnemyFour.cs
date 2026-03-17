using Game.Core.CoreComponents;
using Game.Enemies;
using Game.Enemies.Impls.Enemy1.States;
using Game.Enemies.Impls.Enemy4.States;
using Game.Entities;
using TMPro;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Enemies.Impls.Enemy4
{
    [MovedFrom("")]
    public class EnemyFour : Entity
    {
        [SerializeField] private Transform m_rangeAttackPosition;
        [SerializeField] private Transform[] m_waypoints;

        public override void Awake()
        {
            base.Awake();

            var enemyFourData = data as EnemyFourData;

            fsm.Initialize(
                new EnemyFourIdleState(fsm, core, EnemyAnimationConst.IDLE, this, enemyFourData.idle),
                new EnemyFourMoveState(fsm, core, EnemyAnimationConst.MOVE, this, enemyFourData.move, m_waypoints),
                new EnemyFourLookForPlayerState(fsm, core, EnemyAnimationConst.LOOK_FOR_PLAYER, this, enemyFourData.lookForPlayer),
                new EnemyFourPlayerDetectedState(fsm, core, EnemyAnimationConst.PLAYER_DETECTED, this, enemyFourData.playerDetected),
                new EnemyFourAttackState(fsm, core, EnemyAnimationConst.RANGED_ATTACK, this, m_rangeAttackPosition, enemyFourData.rangeAttack));

            animationToFSM.Initialize(fsm);

            fsm.ChangeState<EnemyFourIdleState>();
        }
    }
}

