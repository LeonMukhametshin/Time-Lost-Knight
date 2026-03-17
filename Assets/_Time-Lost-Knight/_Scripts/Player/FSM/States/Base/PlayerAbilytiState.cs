using Game.Core.CoreComponents;
using Game.Player;
using Game.Player.FSM;
using Game.Player.FSM.Data;
using Game.Player.FSM.States.Impls;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Player.FSM.States.Base
{
    [MovedFrom("")]
    public class PlayerAbilytiState : PlayerState
    {
        protected Movement movement =>
            m_movement ??= core.GetCoreComponent<Movement>();

        protected FlipContoller flipController =>
            m_flipContoller ??= core.GetCoreComponent<FlipContoller>();

        protected PlayerCollisionDetector collisionDetector =>
            m_collisionDetector ??= core.GetCoreComponent<PlayerCollisionDetector>();

        private Movement m_movement;
        private FlipContoller m_flipContoller;
        private PlayerCollisionDetector m_collisionDetector;

        protected bool isAbilityDone;
        private bool m_isGrounded;

        public PlayerAbilytiState(EntityFSM fsm, CoreSystem core,
            string animBoolName, PlayerController player,
            PlayerData data, bool active)
            : base(fsm, core,
                animBoolName, player,
                data, active)
        {
        }

        public override void DoCheck()
        {
            base.DoCheck();

            m_isGrounded = collisionDetector.CheckGrounded();
        }

        public override void Enter()
        {
            base.Enter();

            isAbilityDone = false;
        }

        public override void Update()
        {
            base.Update();

            if (!isAbilityDone)
            {
                return;
            }

            if (m_isGrounded && movement.currentVelocity.y < 0.1f)
            {
                fsm.ChangeState<PlayerIdleState>();
            }
            else
            {
                fsm.ChangeState<PlayerAirState>();
            }
        }
    }
}

