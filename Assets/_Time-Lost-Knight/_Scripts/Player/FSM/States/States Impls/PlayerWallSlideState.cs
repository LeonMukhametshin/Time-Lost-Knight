public class PlayerWallSlideState : PlayerWallTouchingState
{
    protected Movement movement
    {
        get => m_movement ??= core.GetCoreComponent<Movement>();
    }
    private Movement m_movement;

    public PlayerWallSlideState(Player player, EntityFSM fsm,
        PlayerData playerData, string animBoolName) 
        : base(player, fsm, playerData, animBoolName)
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
            fsm.SetState(player.statesContainer.GetState<PlayerWallGrabState>());
        }
    }
}