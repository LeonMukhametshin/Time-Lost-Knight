public class PlayerWallTouchingState : PlayerState
{
    protected int xInput;
    protected int yInput;

    protected bool isGrounded;
    protected bool isTouchingWall;
    protected bool isTouchingLedge;
    protected bool grabInput;
    protected bool jumpInput;

    public PlayerWallTouchingState(Player player, PlayerFSM fsm, 
        PlayerData playerData, string animBoolName) 
        : base(player, fsm, playerData, animBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();

        isGrounded = core.collisionDetector.CheckGrounded();
        isTouchingWall = core.collisionDetector.CheckWallTouch();
        isTouchingLedge = core.collisionDetector.CheckTouchingLedge();

        if(isTouchingWall && !isTouchingLedge)
        {
            player.statesContainer.GetState<PlayerLedgeClibmState>()
                .SetDetectedPosition(player.transform.position);
        }
    }

    public override void Update()
    {
        base.Update();

        CheckInputs();

        if (jumpInput)
        {
            var wallJumpState = player.statesContainer.GetState<PlayerWallJumpState>();
            wallJumpState.DetermineWallJumpDirection(isTouchingWall);
            fsm.SetState(wallJumpState);
        }
        else if (isGrounded && !grabInput)
        {
            fsm.SetState(player.statesContainer.GetState<PlayerIdleState>());
        }
        else if (!isTouchingWall || (xInput != core.flipController.facingDirection  && !grabInput))
        {
            fsm.SetState(player.statesContainer.GetState<PlayerInAirState>());
        }
        else if(isTouchingWall && !isTouchingLedge)
        {
            fsm.SetState(player.statesContainer.GetState<PlayerLedgeClibmState>());
        }
    }

    private void CheckInputs()
    {
        xInput = player.inputHandler.normalizedInputX;
        yInput = player.inputHandler.normalizedInputY;
        grabInput = player.inputHandler.grabInput;
        jumpInput = player.inputHandler.jumpInput;
    }
}