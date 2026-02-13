public class PlayerWallClimbState : PlayerWallTouchingState
{
    public PlayerWallClimbState(Player player, PlayerFSM fsm, 
        PlayerData playerData, string animBoolName) 
        : base(player, fsm, playerData, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if(isExitingState)
        {
            return;
        }

        player.movement.SetVelocityY(data.wallClimbVelocity);

        if (yInput != 1)
        {
            fsm.SetState(player.statesContainer.GetState<PlayerWallGrabState>());
        }
    }
}