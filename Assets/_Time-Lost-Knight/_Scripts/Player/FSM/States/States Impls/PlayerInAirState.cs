using UnityEngine;

public class PlayerInAirState : PlayerState
{
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

    public PlayerInAirState(Player player, PlayerFSM fsm, 
        PlayerData playerData, string animBoolName) 
        : base(player, fsm, playerData, animBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();

        m_oldIsTouchingWall = m_isTouchingWall;
        m_oldIsTouchingWallBack = m_isTouchingWallBack;

        m_isGrounded = core.collisionDetector.CheckGrounded();
        m_isTouchingWall = core.collisionDetector.CheckWallTouch();
        m_isTouchingWallBack = core.collisionDetector.CheckWallTouchBask();
        m_isTouchingLedge = core.collisionDetector.CheckTouchingLedge();

        if(m_isTouchingWall && !m_isTouchingLedge)
        {
            player.statesContainer.GetState<PlayerLedgeClibmState>().SetDetectedPosition(player.transform.position);
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

        var jumpState = player.statesContainer.GetState<PlayerJumpState>();
        var dashState = player.statesContainer.GetState<PlayerDashState>();

        if (player.inputHandler.attackInputs[(int)CombatInputs.primary])
        {
            fsm.SetState(player.statesContainer.GetState<PlayerPrimaryAttackState>());
        }
        else if (player.inputHandler.attackInputs[(int)CombatInputs.secondary])
        {
            fsm.SetState(player.statesContainer.GetState<PlayerSecondaryAttackState>());
        }
        else if(m_isGrounded && core.movement.currentVelocity.y < 0.1f)
        {
            fsm.SetState(player.statesContainer.GetState<PlayerLandState>());
        }
        else if(m_isTouchingWall && !m_isTouchingLedge && !m_isGrounded)
        {
            fsm.SetState(player.statesContainer.GetState<PlayerLedgeClibmState>());
        }
        else if (m_jumpInput && (m_isTouchingWall || m_isTouchingWallBack || m_wallJumpCoyoteTime))
        {
            StopWallJumpCoyoteTime();
            m_isTouchingWall = core.collisionDetector.CheckWallTouch();

            var wallJumpState = player.statesContainer.GetState<PlayerWallJumpState>();
            wallJumpState.DetermineWallJumpDirection(m_isTouchingWall);
            fsm.SetState(wallJumpState);
        }
        else if (m_jumpInput && jumpState.CanJump())
        {                       
            fsm.SetState(jumpState);
        }
        else if (m_isTouchingWall && m_grabInput && m_isTouchingLedge)
        {
            fsm.SetState(player.statesContainer.GetState<PlayerWallGrabState>());
        }
        else if (m_isTouchingWall && m_xInput == core.collisionDetector.facingDirection 
            && core.movement.currentVelocity.y <= 0)
        {
            fsm.SetState(player.statesContainer.GetState<PlayerWallSlideState>());
        }
        else if(m_dashInput && dashState.CheckIfCanDash())
        {
            fsm.SetState(dashState);
        }
        else
        {
            core.flipController.CheckIfShoudFlip(m_xInput);
            core.movement.SetVelocityX(data.movementSpeed * m_xInput);

            player.animationController.animator
                .SetFloat(PlayerAnimation—onstants.Y_VELOCITY, core.movement.currentVelocity.y);
            player.animationController.animator
                .SetFloat(PlayerAnimation—onstants.X_VELOCITY, Mathf.Abs(core.movement.currentVelocity.x));
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
                core.movement.SetVelocityY(core.movement.currentVelocity.y * data.jumpHeightMultiplier);
                m_isJumping = false;
            }
            else if (core.movement.currentVelocity.y <= 0f)
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
            player.statesContainer.GetState<PlayerJumpState>().DecreaseAmountOfJumpLeft();
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