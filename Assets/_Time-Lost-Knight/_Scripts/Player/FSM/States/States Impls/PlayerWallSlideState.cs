public class PlayerWallSlideState : PlayerWallTouchingState
{
    protected Movement movement =>
        m_movement ??= core.GetCoreComponent<Movement>();

    private Movement m_movement;

    public PlayerWallSlideState(EntityFSM fsm, Core core, 
        string animBoolName, Player player, 
        PlayerData data) 
        : base(fsm, core, animBoolName, player, data)
    {
    }

    public override void Update()
    {
        base.Update();
        movement.SetVelocityY(data.wallSlideVelocity);

        if (isExitingState)
        {
            return;
        }

        if (grabInput && yInput == 0)
        {
            fsm.ChangeState<PlayerWallGrabState>();
        }
    }
}