using Game.Core.CoreComponents;
using Game.Enemies.States;
using Game.Enemies.States.Datas;
using Game.Entities;
using Game.Player.FSM;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Enemies.Impls.Enemy2.States
{
    [MovedFrom("")]
    public class EnemyTwoMeleeAttackState : MeleeAttackState
    {
        public EnemyTwoMeleeAttackState(EntityFSM fsm, CoreSystem core,
            string animBoolName, Entity entity,
            Transform attackPosition, MeleeAttackStateData data)
            : base(fsm, core, animBoolName, entity, attackPosition, data)
        {
        }

        public override void Update()
        {
            base.Update();

            if (!isAnimationFinished)
            {
                return;
            }

            if (isPlayerInMinAgroRange)
            {
                fsm.ChangeState<EnemyTwoPlayerDetectedState>();
            }
            else if (!isPlayerInMinAgroRange)
            {
                fsm.ChangeState<EnemyTwoLookForPlayerState>();
            }
        }
    }
}

