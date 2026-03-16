using Game.Core.CoreComponents;
using Game.Player;
using Game.Player.FSM;
using Game.Player.FSM.Data;
using Game.Player.FSM.States.Base;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Player.FSM.States.Impls
{
    [MovedFrom("")]
    public class PlayerMoveState : PlayerGroundState
    {
        protected FlipContoller flipController =>
            m_flipContoller ??= core.GetCoreComponent<FlipContoller>();

        private FlipContoller m_flipContoller;

        public PlayerMoveState(EntityFSM fsm, CoreSystem core,
            string animBoolName, PlayerController player,
            PlayerData data, bool active)
            : base(fsm, core, animBoolName,
                player, data, active)
        {
        }

        public override void Update()
        {
            base.Update();

            movement.SetVelocityXSmooth(data.movementSpeed * xInput, data.movementAcceleration, data.movementDeceleration);
            flipController.CheckIfShoudFlip(xInput);

            if(isExitingState)
            {
                return;
            }

            if (xInput == 0)
            {
                fsm.ChangeState<PlayerIdleState>();
            }
            else if (yInput == -1)
            {
                fsm.ChangeState<PlayerCrouchIdleState>();
            }
        }
    }
}

