public class PlayerWallSlideState : PlayerWallTouchingState
{
    public PlayerWallSlideState(Player player, PlayerFSM fsm,
        PlayerData playerData, string animBoolName) 
        : base(player, fsm, playerData, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();
        player.movement.SetVelocityY(data.wallSlideVelocity);

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