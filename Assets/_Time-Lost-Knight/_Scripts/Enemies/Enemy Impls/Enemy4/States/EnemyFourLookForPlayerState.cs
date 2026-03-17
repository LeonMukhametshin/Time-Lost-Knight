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
    public class EnemyFourLookForPlayerState : LookForPlayerState
    {
        private Vector2? isInAgroZone;

        public EnemyFourLookForPlayerState(EntityFSM fsm, CoreSystem core,
            string animBoolName, Entity entity,
            LookForPlayerStateData data)
            : base(fsm, core, animBoolName, entity, data)
        {
        }

        public override void DoCheck()
        {
            base.DoCheck();

            isInAgroZone = enemyCollisionDetector.GetPlayerPositionInZone();
        }

        public override void Update()
        {
            base.Update();

            if(isInAgroZone is not null)
            {
                fsm.ChangeState<EnemyFourAttackState>();
            }
            else if(isAllTurnsTimeDone)
            {
                fsm.ChangeState<EnemyFourMoveState>();
            }
        }
    }
}

