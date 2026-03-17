using Game.Core.CoreComponents;
using Game.Enemies.States;
using Game.Enemies.States.Datas;
using Game.Entities;
using Game.Player.FSM;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Enemies.Impls.Enemy4.States
{
    [MovedFrom("")]
    public class EnemyFourPlayerDetectedState : PlayerDetectedState
    {
        public EnemyFourPlayerDetectedState(EntityFSM fsm, CoreSystem core, string animBoolName,
            Entity entity, PlayerDetectedData data)
            : base(fsm, core, animBoolName, entity, data)
        {
        }

        public override void Update()
        {
            base.Update();

            if (performeLongRangeAction)
            {
                fsm.ChangeState<EnemyFourAttackState>();
            }
            else if (!isPlayerInMaxAgroRange)
            {
                fsm.ChangeState<EnemyFourLookForPlayerState>();
            }
        }
    }
}

