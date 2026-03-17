using Game.Core.CoreComponents;
using Game.Enemies.States;
using Game.Enemies.States.Datas;
using Game.Entities;
using Game.Player.FSM;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Enemies.Impls.Enemy1.States
{
    [MovedFrom("")]
    public class EnemyFirstChargeState : ChargeState
    {
        public EnemyFirstChargeState(EntityFSM fsm, CoreSystem core,
            string animBoolName, Entity entity,
            ChargeStateData data)
            : base(fsm, core, animBoolName, entity, data)
        {
        }

        public override void Update()
        {
            base.Update();

            if (performCloseRangeAction)
            {
                fsm.ChangeState<EnemyFirstMeleeAttackState>();
            }
            else if (!isDetectingLedge || isDetectingWall)
            {
                fsm.ChangeState<EnemyFirstLookForPlayerState>();
            }
            else if (isChargeTimeOver)
            {
                if(isPlayerInMinAgroRange)
                {
                    fsm.ChangeState<EnemyFirstPlayerDetectedState>();
                }
                else
                {
                    fsm.ChangeState<EnemyFirstLookForPlayerState>();
                }
            }
        }
    }
}

