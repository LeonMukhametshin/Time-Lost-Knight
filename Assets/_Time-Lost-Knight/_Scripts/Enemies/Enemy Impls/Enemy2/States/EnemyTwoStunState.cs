using Game.Core.CoreComponents;
using Game.Enemies.States;
using Game.Enemies.States.Datas;
using Game.Entities;
using Game.Player.FSM;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Enemies.Impls.Enemy2.States
{
    [MovedFrom("")]
    public class EnemyTwoStunState : StanState
    {
        public EnemyTwoStunState(EntityFSM fsm, CoreSystem core,
            string animBoolName, Entity entity,
            StunStateData data)
            : base(fsm, core, animBoolName, entity, data)
        {
        }

        public override void Update()
        {
            base.Update();

            if (!isStunTimeOver)
            {
                return;
            }

            if (isPlayerInMinAgroRange)
            {
                fsm.ChangeState<EnemyTwoPlayerDetectedState>();
            }
            else
            {
                fsm.ChangeState<EnemyTwoLookForPlayerState>();
            }
        }
    }
}

