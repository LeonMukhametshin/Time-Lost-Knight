using Game.Core.CoreComponents;
using Game.Enemies.States;
using Game.Enemies.States.Datas;
using Game.Entities;
using Game.Player.FSM;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Enemies.Impls.Enemy1.States
{
    [MovedFrom("")]
    public class EnemyFirstIdleState : IdleState
    {
        public EnemyFirstIdleState(EntityFSM fsm, CoreSystem core,
            string animBoolName, Entity entity,
            IdleStateData data)
            : base(fsm, core, animBoolName, entity, data)
        {
        }

        public override void Update()
        {
            base.Update();

            if(isPlayerInMinAgroRange)
            {
                fsm.ChangeState<EnemyFirstPlayerDetectedState>();
            }
            else if(isPlayerInMaxAgroRange)
            {
                fsm.ChangeState<EnemyFirstPlayerDetectedState>();
            }
            else if(isIdleTimeOver)
            {
                fsm.ChangeState<EnemyFirstMoveState>();
            }
        }
    }
}

