using Game.Core.CoreComponents;
using Game.Core.FSM;
using Game.Entities;
using Game.Player.FSM;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Enemies.States
{
    [MovedFrom("")]
    public class AttackState : EnemyState, IAnimationTrigger
    {
        protected Transform attackPosition;

        protected bool isAnimationFinished;
        protected bool isPlayerInMinAgroRange;

        protected Vector2? playerPosition;

        protected Movement movement =>
            m_movement ??= core.GetCoreComponent<Movement>();

        protected EnemyCollisionDetector enemyCollisionDetector =>
            m_enemyCollisionDetector ??= core.GetCoreComponent<EnemyCollisionDetector>();

        private Movement m_movement;
        private EnemyCollisionDetector m_enemyCollisionDetector;

        public AttackState(EntityFSM fsm, CoreSystem core,
            string animBoolName, Entity entity, Transform attackPosition)
            : base(fsm, core, animBoolName, entity)
        {
            this.attackPosition = attackPosition;
        }

        public override void DoCheck()
        {
            base.DoCheck();

            isPlayerInMinAgroRange = enemyCollisionDetector.CheckPlayerInMinAgroRange();
        }

        public override void Enter()
        {
            base.Enter();

            isAnimationFinished = false;
            movement.SetVelocityX(0f);

            CheckPlayerPosition();
        }

        public virtual void CheckPlayerPosition()
        {
            playerPosition = enemyCollisionDetector.GetPlayerPositionInMaxAgroRange();
        }

        public virtual void TriggerAnimation() { }

        public virtual void FinishAnimation() =>
            isAnimationFinished = true;

        protected void SetLayer(GameObject visualEffect) =>
            visualEffect.layer = entity.gameObject.layer;
    }
}
