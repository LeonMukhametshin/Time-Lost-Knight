public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(Player player, PlayerFSM fsm, 
        PlayerData playerData, string animBoolName)
        : base(player, fsm, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.movement.SetVelocityX(0f);
    }

    public override void Update()
    {
        base.Update();

        if (isExitingState)
        {
            return; 
        }

        if (xInput != 0)
        {
            fsm.SetState(player.statesContainer.GetState<PlayerMoveState>());
        }
        else if (yInput == -1)
        {
            fsm.SetState(player.statesContainer.GetState<PlayerCrouchIdleState>());
        }
    }
}