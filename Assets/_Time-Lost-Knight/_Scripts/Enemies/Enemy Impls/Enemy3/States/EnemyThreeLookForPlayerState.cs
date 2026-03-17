using Game.Core.CoreComponents;
using Game.Enemies.States;
using Game.Enemies.States.Datas;
using Game.Entities;
using Game.Player.FSM;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Enemies.Impls.Enemy3.States
{
    [MovedFrom("")]
    public class EnemyThreeLookForPlayerState : LookForPlayerState
    {
        public EnemyThreeLookForPlayerState(EntityFSM fsm, CoreSystem core, string animBoolName,
            Entity entity, LookForPlayerStateData data)
            : base(fsm, core, animBoolName, entity, data)
        {
        }

        public override void Update()
        {
            base.Update();

            if (isPlayerInMinAgroRange || isPlayerInMaxAgroRange)
            {
                fsm.ChangeState<EnemyThreePlayerDetectedState>();
            }
            else if (isAllTurnsTimeDone)
            {
                fsm.ChangeState<EnemyThreeIdleState>();
            }
        }
    }
}

