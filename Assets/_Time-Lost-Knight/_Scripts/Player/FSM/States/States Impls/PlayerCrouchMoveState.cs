using Game.Core.CoreComponents;
using Game.Player;
using Game.Player.FSM;
using Game.Player.FSM.Data;
using Game.Player.FSM.States.Base;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Player.FSM.States.Impls
{
    [MovedFrom("")]
    public class PlayerCrouchMoveState : PlayerGroundState
    {
        protected FlipContoller flipController =>
            m_flipContoller ??= core.GetCoreComponent<FlipContoller>();

        private FlipContoller m_flipContoller;

        public PlayerCrouchMoveState(EntityFSM fsm, CoreSystem core,
            string animBoolName, PlayerController player,
            PlayerData data, bool active)
            : base(fsm, core,
                animBoolName, player,
                data, active)
        {
        }

        public override void Update()
        {
            base.Update();

            if (isExitingState)
            {
                return;
            }
            movement.SetVelocityXSmooth(data.crouchMovementVelocity * xInput, data.movementAcceleration, data.movementDeceleration);
            flipController.CheckIfShoudFlip(xInput);

            if (xInput == 0)
            {
                fsm.ChangeState<PlayerCrouchIdleState>();
            }
            else if (yInput != -1 && !isTouchingCeiling)
            {
                fsm.ChangeState<PlayerMoveState>();
            }
        }
    }
}

