using Game.Core.CoreComponents;
using Game.Enemies.States;
using Game.Enemies.States.Datas;
using Game.Entities;
using Game.Player.FSM;
using Unity.VisualScripting.FullSerializer;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Enemies.Impls.Enemy3.States
{
    [MovedFrom("")]
    public class EnemyThreePlayerDetectedState : PlayerDetectedState
    {
        public EnemyThreePlayerDetectedState(EntityFSM fsm, CoreSystem core, string animBoolName,
            Entity entity, PlayerDetectedData data)
            : base(fsm, core, animBoolName, entity, data)
        {
        }

        public override void Update()
        {
            base.Update();

            if(performeCloseRangeAction)
            {
                fsm.ChangeState<EnemyThreeRangeAttackState>();
            }
            else if(performeLongRangeAction)
            {
                fsm.ChangeState<EnemyThreeRangeAttackState>();
            }
            else if (!isPlayerInMaxAgroRange)
            {
                fsm.ChangeState<EnemyThreeLookForPlayerState>();
            }
        }
    }
}

