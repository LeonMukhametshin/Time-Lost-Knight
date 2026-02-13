public class PlayerMoveState : PlayerGroundState
{
    public PlayerMoveState(Player player, PlayerFSM fsm,
        PlayerData playerData, string animBoolName) 
        : base(player, fsm, playerData, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        player.movement.SetVelocityX(data.movementSpeed * xInput);
        player.flipController.CheckIfShoudFlip(xInput);

        if(isExitingState)
        {
            return;
        }

        if (xInput == 0)
        {
            fsm.SetState(player.statesContainer.GetState<PlayerIdleState>());
        }
        else if (yInput == -1)
        {
            fsm.SetState(player.statesContainer.GetState<PlayerCrouchIdleState>());
        }
    }
}