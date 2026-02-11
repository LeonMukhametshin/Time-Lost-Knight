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

    public PlayerGroundState(Player player, PlayerFSM fsm, PlayerData playerData, string animBoolName) 
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

        player.statesContainer.jumpState.ResetAmountOfJumpsLeft();
        player.statesContainer.dashState.ResetCanDash();
    }

    public override void Update()
    {
        base.Update();

        CheckInputs();

        if (m_jumpInput && player.statesContainer.jumpState.CanJump() && !isTouchingCeiling)
        {
            fsm.SetState(player.statesContainer.jumpState);
        }
        else if(!m_isGrounded)
        {
            player.statesContainer.airState.StartCoyoteTime();
            fsm.SetState(player.statesContainer.airState);
        }
        else if(m_isTouchingWall && m_grabInput && m_isTouchingLedge)
        {
            fsm.SetState(player.statesContainer.wallGrabState);
        }
        else if (m_dashInput && player.statesContainer.dashState.CheckIfCanDash() && !isTouchingCeiling)
        {
            fsm.SetState(player.statesContainer.dashState);
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