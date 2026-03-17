using Game.Core.CoreComponents;
using Game.Enemies.States;
using Game.Enemies.States.Datas;
using Game.Entities;
using Game.Player.FSM;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Enemies.Impls.Enemy2.States
{
    [MovedFrom("")]
    public class EnemyTwoDodgeState : DodgeState
    {
        public EnemyTwoDodgeState(EntityFSM fsm, CoreSystem core,
            string animBoolName, Entity entity, DodgeStateData data)
            : base(fsm, core, animBoolName, entity, data)
        {
        }

        public override void Update()
        {
            base.Update();

            if (!isDodgeOver)
            {
                return;
            }

            if (isPlayerInMaxAgroRange && performCloseRangeAction)
            {
                fsm.ChangeState<EnemyTwoMeleeAttackState>();
            }
            else if (isPlayerInMaxAgroRange && !performCloseRangeAction)
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

