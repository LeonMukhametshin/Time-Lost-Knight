using Game.Core.CoreComponents;
using Game.Enemies.States;
using Game.Enemies.States.Datas;
using Game.Entities;
using Game.Player.FSM;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Enemies.Impls.Enemy1.States
{
    [MovedFrom("")]
    public class EnemyFirstMoveState : MoveState
    {
        public EnemyFirstMoveState(EntityFSM fsm, CoreSystem core,
            string animBoolName, Entity entity,
            MoveStateData data)
            : base(fsm, core, animBoolName, entity, data)
        {
        }

        public override void Update()
        {
            base.Update();

            if(isPlayerInMinAgroRange)
            {
                fsm.ChangeState<EnemyFirstIdleState>();
            }
            else if (isPlayerInMaxAgroRange)
            {
                fsm.ChangeState<EnemyFirstPlayerDetectedState>();
            }
            else if(isDetactingWall || !isDetactingLedge)
            {
                fsm.GetState<EnemyFirstIdleState>().SetFlipAfterIdle(true);
                fsm.ChangeState<EnemyFirstIdleState>();
            }
        }
    }
}

