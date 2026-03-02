public class PlayerLandState : PlayerGroundState
{
    public PlayerLandState(EntityFSM fsm, Core core, 
        string animBoolName, Player player, 
        PlayerData data) 
        : base(fsm, core, animBoolName, player, data)
    {
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
            fsm.ChangeState<PlayerMoveState>();
        }
        else if (isAnimationFinished)
        {
            fsm.ChangeState<PlayerIdleState>();
        }
    }
}