public class PlayerWallClimbState : PlayerWallTouchingState
{
    protected Movement movement
    {
        get => m_movement ??= core.GetCoreComponent<Movement>();
    }
    private Movement m_movement;

    public PlayerWallClimbState(Player player, EntityFSM fsm, 
        PlayerData playerData, string animBoolName) 
        : base(player, fsm, playerData, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        movement.SetVelocityY(data.wallClimbVelocity);

        if (isExitingState)
        {
            return;
        }

        if (yInput != 1)
        {
            fsm.SetState(player.statesContainer.GetState<PlayerWallGrabState>());
        }
    }
}