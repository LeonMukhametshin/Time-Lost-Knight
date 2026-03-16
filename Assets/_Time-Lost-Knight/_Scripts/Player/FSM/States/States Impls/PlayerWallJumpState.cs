using Game.Core.CoreComponents;
using Game.Player;
using Game.Player.FSM;
using Game.Player.FSM.Data;
using Game.Player.FSM.States.Base;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Player.FSM.States.Impls
{
    [MovedFrom("")]
    public class PlayerWallJumpState : PlayerAbilytiState
    {
        private int m_wallJumpDirection;

        public PlayerWallJumpState(EntityFSM fsm, CoreSystem core,
            string animBoolName, PlayerController player,
            PlayerData data, bool active)
            : base(fsm, core,
                animBoolName, player,
                data, active)
        {
        }

        public override void Enter()
        {
            base.Enter();

            var jumpState = fsm.GetState<PlayerJumpState>();
            player.inputHandler.UseJumpInput();
            jumpState.ResetAmountOfJumpsLeft();
            movement.SetVelocity(data.wallJumpVelocity, data.wallJumpAnge, m_wallJumpDirection);
            flipController.CheckIfShoudFlip(m_wallJumpDirection);
            jumpState.DecreaseAmountOfJumpLeft();
        }

        public override void Update()
        {
            base.Update();

            player.animator
                .SetFloat(PlayerAnimationConstants.Y_VELOCITY, movement.currentVelocity.y);
            player.animator
                .SetFloat(PlayerAnimationConstants.X_VELOCITY, Mathf.Abs(movement.currentVelocity.x));

            if(Time.time >= startTime + data.wallJumpTime)
            {
                isAbilityDone = true;
            }
        }

        public void DetermineWallJumpDirection(bool isTouchingWall)
        {
            m_wallJumpDirection = isTouchingWall
                ? -flipController.facingDirection
                : flipController.facingDirection;
        }
    }
}

