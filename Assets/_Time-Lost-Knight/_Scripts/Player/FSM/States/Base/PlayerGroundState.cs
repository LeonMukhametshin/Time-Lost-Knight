public class PlayerGroundState : PlayerState
{
    protected int xInput;
    protected int yInput;

    protected bool isTouchingCeiling;

    private bool m_jumpInput;
    private bool m_isGrounded;
    private bool m_isTouchingWall;
    private bool m_grabInput;
    private bool m_isTouchingLedge;
    private bool m_dashInput;

    private bool m_dropDownInput;

    public PlayerGroundState(Player player, PlayerFSM fsm, 
        PlayerData playerData, string animBoolName) 
        : base(player, fsm, playerData, animBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();

        m_isGrounded = player.collisionDetector.CheckGrounded();
        m_isTouchingWall = player.collisionDetector.CheckWallTouch();
        m_isTouchingLedge = player.collisionDetector.CheckTouchingLedge();
        isTouchingCeiling = player.collisionDetector.CheckCeilingCheck();
    }

    public override void Enter()
    {
        base.Enter();

        player.statesContainer.GetState<PlayerJumpState>().ResetAmountOfJumpsLeft();
        player.statesContainer.GetState<PlayerDashState>().ResetCanDash();
    }

    public override void Update()
    {
        base.Update();

        CheckInputs();

        m_dropDownInput = player.inputHandler.dropDownInput;

        if(m_jumpInput && player.statesContainer.GetState<PlayerJumpState>().CanJump() && !isTouchingCeiling)
        {
            fsm.SetState(player.statesContainer.GetState<PlayerJumpState>());
        }
        else if (m_dropDownInput)
        {
            player.inputHandler.UseDropDownInput();
            fsm.SetState(player.statesContainer.GetState<PlayerDropDownState>());
        }
        else if(!m_isGrounded)
        {
            var airState = player.statesContainer.GetState<PlayerInAirState>();
            airState.StartCoyoteTime();
            fsm.SetState(airState);
        }
        else if(m_isTouchingWall && m_grabInput && m_isTouchingLedge)
        {
            fsm.SetState(player.statesContainer.GetState<PlayerWallGrabState>());
        }
        else if (m_dashInput && player.statesContainer.GetState<PlayerDashState>().CheckIfCanDash() && !isTouchingCeiling)
        {
            fsm.SetState(player.statesContainer.GetState<PlayerDashState>());
        }
    }

    private void CheckInputs()
    {
        xInput = player.inputHandler.normalizedInputX;
        yInput = player.inputHandler.normalizedInputY;

        m_jumpInput = player.inputHandler.jumpInput;
        m_grabInput = player.inputHandler.grabInput;
        m_dashInput = player.inputHandler.dashInput;
    }
}