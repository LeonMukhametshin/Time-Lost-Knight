using Game.Core.CoreComponents;
using Game.Core.FSM;
using Game.Enemies.States.Datas;
using Game.Entities;
using Game.Player.FSM;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Enemies.States
{
    [MovedFrom("")]
    public class MoveState : EnemyState
    {
        protected MoveStateData data;

        protected bool isDetactingWall;
        protected bool isDetactingLedge;
        protected bool isPlayerInMinAgroRange;
        protected bool isPlayerInMaxAgroRange;

        protected Movement movement =>
            m_movement ??= core.GetCoreComponent<Movement>();

        protected FlipContoller flipController =>
            m_flipContoller ??= core.GetCoreComponent<FlipContoller>();

        protected EnemyCollisionDetector enemyCollisionDetector =>
            m_enemyCollisionDetector ??= core.GetCoreComponent<EnemyCollisionDetector>();

        private Movement m_movement;
        private FlipContoller m_flipContoller;
        private EnemyCollisionDetector m_enemyCollisionDetector;

        public MoveState(EntityFSM fsm, CoreSystem core,
            string animBoolName, Entity entity,
            MoveStateData data)
            : base(fsm, core, animBoolName, entity)
        {
            this.data = data;
        }

        public override void DoCheck()
        {
            isDetactingLedge = enemyCollisionDetector.CheckLedge();
            isDetactingWall = enemyCollisionDetector.CheckWallTouch();
            isPlayerInMinAgroRange = enemyCollisionDetector.CheckPlayerInMinAgroRange();
            isPlayerInMaxAgroRange = enemyCollisionDetector.CheckPlayerInMaxAgroRange();
        }

        public override void Enter()
        {
            base.Enter();
            Move();
        }

        public override void Update()
        {
            base.Update();
            Move();
        }

        protected virtual void Move() =>
            movement.SetVelocityX(data.movementSpeed * flipController.facingDirection);
    }
}
