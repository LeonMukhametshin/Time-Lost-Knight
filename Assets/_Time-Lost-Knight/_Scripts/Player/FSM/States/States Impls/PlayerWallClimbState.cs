public class PlayerWallClimbState : PlayerWallTouchingState
{
    public PlayerWallClimbState(EntityFSM fsm, Core core, 
        string animBoolName, Player player, 
        PlayerData data) 
        : base(fsm, core, animBoolName, player, data)
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
            fsm.ChangeState<PlayerWallGrabState>();
        }
    }
}