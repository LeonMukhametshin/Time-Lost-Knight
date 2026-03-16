using Game.Core.CoreComponents;
using Game.Enemies.States;
using Game.Enemies.States.Datas;
using Game.Entities;
using Game.Player.FSM;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Enemies.Impls.Enemy2.States
{
    [MovedFrom("")]
    public class EnemyTwoPlayerDetectedState : PlayerDetectedState
    {
        public EnemyTwoPlayerDetectedState(EntityFSM fsm, CoreSystem core,
            string animBoolName,
            Entity entity, PlayerDetectedData data)
            : base(fsm, core, animBoolName, entity, data)
        {
        }

        public override void Update()
        {
            base.Update();

            if(performeCloseRangeAction)
            {
                var dodgeState = fsm.GetState<EnemyTwoDodgeState>();
                if(Time.time >= dodgeState.startTime
                    + dodgeState.data.dodgeCooldown)
                {
                    fsm.ChangeState<EnemyTwoDodgeState>();
                }
                else
                {
                    fsm.ChangeState<EnemyTwoMeleeAttackState>();
                }
            }
            else if(performeLongRangeAction)
            {
                fsm.ChangeState<EnemyTwoRangeAttackState>();
            }
            else if (!isPlayerInMaxAgroRange)
            {
                fsm.ChangeState<EnemyTwoLookForPlayerState>();
            }
        }
    }
}

