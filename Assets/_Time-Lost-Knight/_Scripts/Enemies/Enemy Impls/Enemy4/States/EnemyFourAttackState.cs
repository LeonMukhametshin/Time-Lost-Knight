using Game.Core.CoreComponents;
using Game.Enemies.States;
using Game.Enemies.States.Datas;
using Game.Entities;
using Game.Player.FSM;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Enemies.Impls.Enemy4.States
{
    [MovedFrom("")]
    public class EnemyFourAttackState : RangeAttackState
    {
        private Vector2? isInAgroZone;

        public EnemyFourAttackState(EntityFSM fsm, CoreSystem core,
            string animBoolName, Entity entity,
            Transform attackPosition, RangeAttackData data)
            : base(fsm, core, animBoolName, entity,
                attackPosition, data)
        {
        }

        public override void DoCheck()
        {
            base.DoCheck();

            isInAgroZone = enemyCollisionDetector.GetPlayerPositionInZone();
            CheckPlayerPosition();
        }

        public override void Update()
        {
            base.Update();

            if (!isAnimationFinished)
            {
                return;
            }

            if (isInAgroZone is not null)
            {
                fsm.ChangeState<EnemyFourPlayerDetectedState>();
            }
            else
            {
                fsm.ChangeState<EnemyFourLookForPlayerState>();
            }
        }

        public override void CheckPlayerPosition()
        {
            playerPosition = enemyCollisionDetector.GetPlayerPositionInZone();
        }

        protected override Vector2 CalculateShotDirection() =>
            playerPosition.Value;
    }
}

