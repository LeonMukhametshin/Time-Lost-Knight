using Game.Core.CoreComponents;
using Game.Player;
using Game.Player.FSM;
using Game.Player.FSM.Data;
using Game.Player.Input;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.Windows;

namespace Game.Player.FSM.States.Impls
{
    [MovedFrom("")]
    public class PlayerAirState : PlayerState
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

        private int m_xInput;
        private bool m_dashInput;
        private bool m_grabInput;
        private bool m_jumpInput;
        private bool m_jumpInputStop;

        private bool m_isGrounded;

        private bool m_oldIsTouchingWall;
        private bool m_oldIsTouchingWallBack;
        private bool m_isTouchingWall;

        private bool m_isTouchingWallBack;
        private bool m_isTouchingLedge;

        private bool m_coyoteTime;
        private bool m_isJumping;
        private bool m_wallJumpCoyoteTime;
        private float m_startWallJumpCoyoteTime;

        public PlayerAirState(EntityFSM fsm, CoreSystem core,
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

            m_oldIsTouchingWall = m_isTouchingWall;
            m_oldIsTouchingWallBack = m_isTouchingWallBack;

            m_isGrounded = collisionDetector.CheckGrounded();
            m_isTouchingWall = collisionDetector.CheckWallTouch();
            m_isTouchingWallBack = collisionDetector.CheckWallTouchBack();
            m_isTouchingLedge = collisionDetector.CheckTouchingLedge();

            if(m_isTouchingWall && !m_isTouchingLedge)
            {
                fsm.GetState<PlayerLedgeClibmState>()
                    .SetDetectedPosition(player.transform.position);
            }

            if(!m_wallJumpCoyoteTime && !m_isTouchingWall && !m_isTouchingWallBack
                && (m_oldIsTouchingWall || m_oldIsTouchingWallBack))
            {
                StartWallJumpCoyoteTime();
            }
        }

        public override void Exit()
        {
            base.Exit();

            m_oldIsTouchingWall = false;
            m_oldIsTouchingWallBack = false;
            m_isTouchingWall = false;
            m_isTouchingWallBack = false;
        }

        public override void Update()
        {
            base.Update();

            CheckCoyoteTime();
            CheckWallJumpCoyoteTime();
            CheckInputs();
            CheckJumpMultiplier();

            if (player.inputHandler.attackInputs[(int)CombatInputs.primary])
            {
                fsm.ChangeState<PlayerPrimaryAttackState>();
            }
            else if (m_jumpInput && fsm.GetState<PlayerJumpState>().CanJump())
            {
                fsm.ChangeState<PlayerJumpState>();
            }
            else if(m_isGrounded && movement.currentVelocity.y < 0.1f)
            {
                fsm.ChangeState<PlayerLandState>();
            }
            else if(m_isTouchingWall && !m_isTouchingLedge && !m_isGrounded)
            {
                fsm.ChangeState<PlayerLedgeClibmState>();;
            }
            else if (m_jumpInput && (m_isTouchingWall || m_isTouchingWallBack || m_wallJumpCoyoteTime))
            {
                StopWallJumpCoyoteTime();
                m_isTouchingWall = collisionDetector.CheckWallTouch();

                var wallJumpState = fsm.GetState<PlayerWallJumpState>();;
                wallJumpState.DetermineWallJumpDirection(m_isTouchingWall);
                fsm.ChangeState<PlayerWallJumpState>();
            }
            else if (m_isTouchingWall && m_grabInput && m_isTouchingLedge)
            {
                fsm.ChangeState<PlayerWallGrabState>();
            }
            else if (m_isTouchingWall && m_xInput == flipController.facingDirection
                && movement.currentVelocity.y <= 0)
            {
                fsm.ChangeState<PlayerWallSlideState>();
            }
            else if(m_dashInput && collisionDetector.CheckForOmnidirectionalZone())
            {
                fsm.ChangeState<PlayerOmnidirectionalDashState>();
            }
            else if(m_dashInput && fsm.GetState<PlayerForwardDashState>().CheckIfCanDash())
            {
                fsm.ChangeState<PlayerForwardDashState>();
            }
            else
            {
                flipController.CheckIfShoudFlip(m_xInput);
                movement.SetVelocityXSmooth(data.movementSpeed * m_xInput, data.movementAcceleration, data.movementDeceleration);

                player.animator
                    .SetFloat(PlayerAnimationConstants.Y_VELOCITY, movement.currentVelocity.y);
                player.animator
                    .SetFloat(PlayerAnimationConstants.X_VELOCITY, Mathf.Abs(movement.currentVelocity.x));
            }
        }

        private void CheckInputs()
        {
            m_xInput = player.inputHandler.normalizedInputX;
            m_jumpInput = player.inputHandler.jumpInput;
            m_jumpInputStop = player.inputHandler.jumpInputStop;
            m_grabInput = player.inputHandler.grabInput;
            m_dashInput = player.inputHandler.dashInput;
        }

        private void CheckJumpMultiplier()
        {
            if (m_isJumping)
            {
                if (m_jumpInputStop)
                {
                    movement.SetVelocityY(movement.currentVelocity.y * Mathf.Clamp01(data.jumpHeightMultiplier));
                    m_isJumping = false;
                }
                else if (movement.currentVelocity.y <= 0f)
                {
                    m_isJumping = false;
                }
            }
        }

        private void CheckCoyoteTime()
        {
            if(m_coyoteTime && Time.time > startTime + data.coyoteTime)
            {
                m_coyoteTime = false;
                fsm.GetState<PlayerJumpState>().DecreaseAmountOfJumpLeft();
            }
        }

        private void CheckWallJumpCoyoteTime()
        {
            if(m_wallJumpCoyoteTime && Time.time > m_startWallJumpCoyoteTime + data.coyoteTime)
            {
                m_wallJumpCoyoteTime = false;
            }
        }

        public void StartCoyoteTime() =>
            m_coyoteTime = true;

        public void SetIsJumping() =>
            m_isJumping = true;

        public void StopWallJumpCoyoteTime() =>
            m_wallJumpCoyoteTime = false;

        public void StartWallJumpCoyoteTime()
        {
            m_wallJumpCoyoteTime = true;
            m_startWallJumpCoyoteTime = Time.time;
        }
    }
}

