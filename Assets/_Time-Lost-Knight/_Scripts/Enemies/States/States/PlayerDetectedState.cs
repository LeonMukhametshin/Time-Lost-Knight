using Game.Core.CoreComponents;
using Game.Core.FSM;
using Game.Enemies.States.Datas;
using Game.Entities;
using Game.Player.FSM;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Enemies.States
{
    [MovedFrom("")]
    public class PlayerDetectedState : EnemyState
    {
        protected PlayerDetectedData data;

        protected bool isPlayerInMinAgroRange;
        protected bool isPlayerInMaxAgroRange;
        protected bool performeLongRangeAction;
        protected bool performeCloseRangeAction;
        protected bool isDetectingLedge;

        private Movement movement =>
            m_movement ??= core.GetCoreComponent<Movement>();

        private EnemyCollisionDetector enemyCollisionDetector =>
            m_enemyCollisionDetector ??= core.GetCoreComponent<EnemyCollisionDetector>();

        private Movement m_movement;
        private EnemyCollisionDetector m_enemyCollisionDetector;

        public PlayerDetectedState(EntityFSM fsm, CoreSystem core,
            string animBoolName, Entity entity,
            PlayerDetectedData data)
            : base(fsm, core, animBoolName, entity)
        {
            this.data = data;
        }

        public override void DoCheck()
        {
            base.DoCheck();

            isDetectingLedge = enemyCollisionDetector.CheckLedge();
            isPlayerInMinAgroRange = enemyCollisionDetector.CheckPlayerInMinAgroRange();
            isPlayerInMaxAgroRange = enemyCollisionDetector.CheckPlayerInMaxAgroRange();
            performeCloseRangeAction = enemyCollisionDetector.CheckPlayerInCloseRangeAction();
        }

        public override void Enter()
        {
            base.Enter();
            performeLongRangeAction = false;
            movement.SetVelocityX(0f);
        }

        public override void Update()
        {
            base.Update();

            if(Time.time >= startTime + data.longRangeActionTime)
            {
                performeLongRangeAction = true;
            }
        }
    }
}
