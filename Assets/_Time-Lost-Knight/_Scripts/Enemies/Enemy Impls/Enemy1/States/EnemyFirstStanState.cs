using Game.Core.CoreComponents;
using Game.Enemies.States;
using Game.Enemies.States.Datas;
using Game.Entities;
using Game.Player.FSM;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Enemies.Impls.Enemy1.States
{
    [MovedFrom("")]
    public class EnemyFirstStanState : StanState
    {
        public EnemyFirstStanState(EntityFSM fsm, CoreSystem core,
            string animBoolName, Entity entity,
            StunStateData data)
            : base(fsm, core, animBoolName, entity, data)
        {
        }

        public override void Update()
        {
            base.Update();

            if(!isStunTimeOver)
            {
                return;
            }

            if (performCloseRangeAction)
            {
                fsm.ChangeState<EnemyFirstMeleeAttackState>();
            }
            else if (isPlayerInMinAgroRange)
            {
                fsm.ChangeState<EnemyFirstChargeState>();
            }
            else
            {
                fsm.GetState<EnemyFirstLookForPlayerState>().SetTurnImmediately(true);
                fsm.ChangeState<EnemyFirstLookForPlayerState>();
            }
        }
    }
}

