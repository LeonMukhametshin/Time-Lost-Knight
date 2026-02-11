public class PlayerWallTouchingState : PlayerState
{
    protected int xInput;
    protected int yInput;

    protected bool isGrounded;
    protected bool isTouchingWall;
    protected bool isTouchingLedge;
    protected bool grabInput;
    protected bool jumpInput;

    public PlayerWallTouchingState(Player player, PlayerFSM fsm, PlayerData playerData, string animBoolName) 
        : base(player, fsm, playerData, animBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();

        isGrounded = player.collisionDetector.CheckGrounded();
        isTouchingWall = player.collisionDetector.CheckWallTouch();
        isTouchingLedge = player.collisionDetector.CheckTouchingLedge();

        if(isTouchingWall && !isTouchingLedge)
        {
            player.statesContainer.playerLedgeClibmState.SetDetectedPosition(player.transform.position);
        }
    }

    public override void Update()
    {
        base.Update();

        CheckInputs();

        if (jumpInput)
        {
            player.statesContainer.wallJumpState.DetermineWallJumpDirection(isTouchingWall);
            fsm.SetState(player.statesContainer.wallJumpState);
        }
        else if (isGrounded && !grabInput)
        {
            fsm.SetState(player.statesContainer.idleState);
        }
        else if (!isTouchingWall || (xInput != player.collisionDetector.facingDirection  && !grabInput))
        {
            fsm.SetState(player.statesContainer.airState);
        }
        else if(isTouchingWall && !isTouchingLedge)
        {
            fsm.SetState(player.statesContainer.playerLedgeClibmState);
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