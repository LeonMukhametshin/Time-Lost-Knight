using UnityEngine;

public class PlayerGroundState : PlayerState
{
    protected Movement movement => 
        m_movement ??= core.GetCoreComponent<Movement>();

    protected PlayerCollisionDetector collisionDetector => 
        m_collisionDetector ??= core.GetCoreComponent<PlayerCollisionDetector>();

    private Movement m_movement;
    private PlayerCollisionDetector m_collisionDetector;

    protected int xInput;
    protected int yInput;

    protected bool isTouchingCeiling;

    private bool m_jumpInput;
    private bool m_isGrounded;
    private bool m_isTouchingWall;
    private bool m_isTouchingPlatform;
    private bool m_grabInput;
    private bool m_isTouchingLedge;
    private bool m_dashInput;
    private bool m_isTouchingOneWayPlatform;

    private Collider2D m_oneWayPlatformCollider;

    private bool m_dropDownInput;

    public PlayerGroundState(EntityFSM fsm, Core core, 
        string animBoolName, Player player, 
        PlayerData data) 
        : base(fsm, core, animBoolName, player, data)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();

        m_isGrounded = collisionDetector.CheckGrounded();
        m_isTouchingWall = collisionDetector.CheckWallTouch();
        m_isTouchingLedge = collisionDetector.CheckTouchingLedge();
        isTouchingCeiling = collisionDetector.CheckCeilingCheck();
        m_isTouchingPlatform = collisionDetector.CheckTouckingPlatform();
    }

    public override void Enter()
    {
        base.Enter();

        fsm.GetState<PlayerJumpState>().ResetAmountOfJumpsLeft();
        fsm.GetState<PlayerForwardDashState>().ResetCanDash();
    }

    public override void Update()
    {
        base.Update();

        CheckInputs();

        if (player.inputHandler.attackInputs[(int)CombatInputs.primary] && !isTouchingCeiling)
        {
            fsm.ChangeState<PlayerPrimaryAttackState>();
        }
        else if (player.inputHandler.attackInputs[(int)CombatInputs.secondary] && !isTouchingCeiling)
        {
            fsm.ChangeState<PlayerSecondaryAttackState>();
        }
        else if (m_jumpInput && fsm.GetState<PlayerJumpState>().CanJump() && !isTouchingCeiling)
        {
            fsm.ChangeState<PlayerJumpState>();
        }
        else if (m_dropDownInput && m_isTouchingPlatform)
        {
            player.inputHandler.UseDropDownInput();
            fsm.ChangeState<PlayerDropDownState>();
        }
        else if(!m_isGrounded && movement.currentVelocity.y < -0.01f)
        {
            var airState = fsm.GetState<PlayerAirState>();
            airState.StartCoyoteTime();
            fsm.ChangeState<PlayerAirState>();
        }
        else if(m_isTouchingWall && m_grabInput && m_isTouchingLedge)
        {
            fsm.ChangeState<PlayerWallGrabState>();
        }
        else if(m_dashInput && collisionDetector.CheckForOmnidirectionalZone() && !isTouchingCeiling)
        {
            fsm.ChangeState<PlayerOmnidirectionalDashState>();
        }
        else if (m_dashInput && fsm.GetState<PlayerForwardDashState>().CheckIfCanDash() && !isTouchingCeiling)
        {
            fsm.ChangeState<PlayerForwardDashState>();
        }
    }

    private void CheckInputs()
    {
        xInput = player.inputHandler.normalizedInputX;
        yInput = player.inputHandler.normalizedInputY;

        m_jumpInput = player.inputHandler.jumpInput;
        m_grabInput = player.inputHandler.grabInput;
        m_dashInput = player.inputHandler.dashInput;
        m_dropDownInput = player.inputHandler.dropDownInput;
    }
}
