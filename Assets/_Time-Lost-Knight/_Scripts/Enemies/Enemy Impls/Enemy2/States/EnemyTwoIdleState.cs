using Game.Core.CoreComponents;
using Game.Enemies.States;
using Game.Enemies.States.Datas;
using Game.Entities;
using Game.Player.FSM;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Enemies.Impls.Enemy2.States
{
    [MovedFrom("")]
    public class EnemyTwoIdleState : IdleState
    {
        public EnemyTwoIdleState(EntityFSM fsm, CoreSystem core,
            string animBoolName, Entity entity, IdleStateData data)
            : base(fsm, core, animBoolName, entity, data)
        {
        }

        public override void Update()
        {
            base.Update();

            if(isPlayerInMinAgroRange)
            {
                fsm.ChangeState<EnemyTwoPlayerDetectedState>();
            }
            else if (isIdleTimeOver)
            {
                fsm.ChangeState<EnemyTwoMoveState>();
            }
        }
    }
}

