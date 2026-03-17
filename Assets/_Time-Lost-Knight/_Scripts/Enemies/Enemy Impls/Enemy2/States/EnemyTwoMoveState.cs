using Game.Core.CoreComponents;
using Game.Enemies.States;
using Game.Enemies.States.Datas;
using Game.Entities;
using Game.Player.FSM;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Enemies.Impls.Enemy2.States
{
    [MovedFrom("")]
    public class EnemyTwoMoveState : MoveState
    {
        public EnemyTwoMoveState(EntityFSM fsm, CoreSystem core,
            string animBoolName, Entity entity, MoveStateData data)
            : base(fsm, core, animBoolName, entity, data)
        {
        }

        public override void Update()
        {
            base.Update();

            if (isPlayerInMinAgroRange)
            {
                fsm.ChangeState<EnemyTwoPlayerDetectedState>();
            }
            else if(isPlayerInMaxAgroRange)
            {
                fsm.ChangeState<EnemyTwoPlayerDetectedState>();
            }
            else if (isDetactingWall || !isDetactingLedge)
            {
                fsm.GetState<EnemyTwoIdleState>().SetFlipAfterIdle(true);
                fsm.ChangeState<EnemyTwoIdleState>();
            }
        }
    }
}

