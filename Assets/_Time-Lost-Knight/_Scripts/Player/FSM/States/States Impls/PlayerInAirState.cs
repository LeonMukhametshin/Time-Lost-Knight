using UnityEngine;

public class PlayerInAirState : PlayerState
{
    protected FlipContoller flipController
    {
        get => m_flipContoller ??= core.GetCoreComponent<FlipContoller>();
    }

    protected Movement movement
    {
        get => m_movement ??= core.GetCoreComponent<Movement>();
    }

    protected PlayerCollisionDetector collisionDetector
    {
        get => m_collisionDetector ??= core.GetCoreComponent<PlayerCollisionDetector>();
    }

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

    public PlayerInAirState(Player player, EntityFSM fsm, 
        PlayerData playerData, string animBoolName) 
        : base(player, fsm, playerData, animBoolName)
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
            player.statesContainer.GetState<PlayerLedgeClibmState>()
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
        else if(m_isGrounded && movement.currentVelocity.y < 0.1f)
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
            m_isTouchingWall = collisionDetector.CheckWallTouch();

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
        else if (m_isTouchingWall && m_xInput == flipController.facingDirection 
            && movement.currentVelocity.y <= 0)
        {
            fsm.SetState(player.statesContainer.GetState<PlayerWallSlideState>());
        }
        else if(m_dashInput && dashState.CheckIfCanDash())
        {
            fsm.SetState(dashState);
        }
        else
        {
            flipController.CheckIfShoudFlip(m_xInput);
            movement.SetVelocityX(data.movementSpeed * m_xInput);

            player.animationController.animator
                .SetFloat(PlayerAnimationConstants.Y_VELOCITY, movement.currentVelocity.y);
            player.animationController.animator
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
                movement.SetVelocityY(movement.currentVelocity.y * data.jumpHeightMultiplier);
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