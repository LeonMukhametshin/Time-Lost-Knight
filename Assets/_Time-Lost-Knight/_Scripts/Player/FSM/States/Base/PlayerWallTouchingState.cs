public class PlayerWallTouchingState : PlayerState
{
    protected Movement movement =>
        m_movement ??= core.GetCoreComponent<Movement>();

    protected FlipContoller flipContoller => 
        m_flipContoller ??= core.GetCoreComponent<FlipContoller>();

    protected PlayerCollisionDetector collisionDetector => 
        m_collisionDetector ??= core.GetCoreComponent<PlayerCollisionDetector>();

    private Movement m_movement;
    private FlipContoller m_flipContoller;
    private PlayerCollisionDetector m_collisionDetector;

    protected int xInput;
    protected int yInput;

    protected bool isGrounded;
    protected bool isTouchingWall;
    protected bool isTouchingLedge;
    protected bool grabInput;
    protected bool jumpInput;

    public PlayerWallTouchingState(EntityFSM fsm, Core core, 
        string animBoolName, Player player, 
        PlayerData data, bool active) 
        : base(fsm, core, 
            animBoolName, player, 
            data, active)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();

        isGrounded = collisionDetector.CheckGrounded();
        isTouchingWall = collisionDetector.CheckWallTouch();
        isTouchingLedge = collisionDetector.CheckTouchingLedge();

        if(isTouchingWall && !isTouchingLedge)
        {
            fsm.GetState<PlayerLedgeClibmState>()
                .SetDetectedPosition(player.transform.position);
        }
    }

    public override void Update()
    {
        base.Update();

        CheckInputs();

        if (jumpInput)
        {
            var wallJumpState = fsm.GetState<PlayerWallJumpState>();
            wallJumpState.DetermineWallJumpDirection(isTouchingWall);
            fsm.ChangeState<PlayerWallJumpState>();
        }
        else if (isGrounded && !grabInput)
        {
            fsm.ChangeState<PlayerIdleState>();
        }
        else if (!isTouchingWall || (xInput != flipContoller.facingDirection  && !grabInput))
        {
            fsm.ChangeState<PlayerAirState>();
        }
        else if(isTouchingWall && !isTouchingLedge)
        {
            fsm.ChangeState<PlayerLedgeClibmState>();
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