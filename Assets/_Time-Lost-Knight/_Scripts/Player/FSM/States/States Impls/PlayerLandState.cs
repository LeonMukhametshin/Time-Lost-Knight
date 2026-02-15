public class PlayerLandState : PlayerGroundState
{
    public PlayerLandState(Player player, PlayerFSM fsm, 
        PlayerData playerData, string animBoolName) 
        : base(player, fsm, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
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
        else if (isAnimationFinished)
        {
            fsm.SetState(player.statesContainer.GetState<PlayerIdleState>());
        }
    }
}