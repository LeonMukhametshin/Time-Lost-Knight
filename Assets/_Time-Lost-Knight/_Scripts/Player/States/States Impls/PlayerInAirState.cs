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

    public PlayerInAirState(Player player, PlayerFSM fsm, PlayerData playerData, string animBoolName) 
        : base(player, fsm, playerData, animBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();

        m_oldIsTouchingWall = m_isTouchingWall;
        m_oldIsTouchingWallBack = m_isTouchingWallBack;

        m_isGrounded = player.collisionDetector.CheckGrounded();
        m_isTouchingWall = player.collisionDetector.CheckWallTouch();
        m_isTouchingWallBack = player.collisionDetector.CheckWallTouchBask();
        m_isTouchingLedge = player.collisionDetector.CheckTouchingLedge();

        if(m_isTouchingWall && !m_isTouchingLedge)
        {
            player.statesContainer.playerLedgeClibmState.SetDetectedPosition(player.transform.position);
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

        if (m_isGrounded && player.movement.currentVelocity.y < 0.1f)
        {
            fsm.SetState(player.statesContainer.landState);
        }
        else if(m_isTouchingWall && !m_isTouchingLedge && !m_isGrounded)
        {
            fsm.SetState(player.statesContainer.playerLedgeClibmState);
        }
        else if (m_jumpInput && (m_isTouchingWall || m_isTouchingWallBack || m_wallJumpCoyoteTime))
        {
            StopWallJumpCoyoteTime();
            m_isTouchingWall = player.collisionDetector.CheckWallTouch();
            player.statesContainer.wallJumpState.DetermineWallJumpDirection(m_isTouchingWall);
            fsm.SetState(player.statesContainer.wallJumpState);
        }
        else if (m_jumpInput && player.statesContainer.jumpState.CanJump())
        {                       
            fsm.SetState(player.statesContainer.jumpState);
        }
        else if (m_isTouchingWall && m_grabInput && m_isTouchingLedge)
        {
            fsm.SetState(player.statesContainer.wallGrabState);
        }
        else if (m_isTouchingWall && m_xInput == player.collisionDetector.facingDirection 
            && player.movement.currentVelocity.y <= 0)
        {
            fsm.SetState(player.statesContainer.wallSlideState);
        }
        else if(m_dashInput && player.statesContainer.dashState.CheckIfCanDash())
        {
            fsm.SetState(player.statesContainer.dashState);
        }
        else
        {
            player.flipController.CheckIfShoudFlip(m_xInput);
            player.movement.SetVelocityX(data.movementSpeed * m_xInput);

            player.animationController.animator.SetFloat(PlayerAnimationConst.Y_VELOCITY, player.movement.currentVelocity.y);
            player.animationController.animator.SetFloat(PlayerAnimationConst.X_VELOCITY, Mathf.Abs(player.movement.currentVelocity.x));
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
                player.movement.SetVelocityY(player.movement.currentVelocity.y * data.jumpHeightMultiplier);
                m_isJumping = false;
            }
            else if (player.movement.currentVelocity.y <= 0f)
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
            player.statesContainer.jumpState.DecreaseAmountOfJumpLeft();
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