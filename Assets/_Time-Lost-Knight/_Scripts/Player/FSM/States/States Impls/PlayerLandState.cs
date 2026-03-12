public class PlayerLandState : PlayerGroundState
{
    public PlayerLandState(EntityFSM fsm, Core core, 
        string animBoolName, Player player, 
        PlayerData data, bool active) 
        : base(fsm, core,
            animBoolName, player, 
            data, active)
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
        else if(movement.rb.linearVelocityY < 0.05f)
        {
            fsm.ChangeState<PlayerIdleState>();
        }
        else if (isAnimationFinished)
        {
            fsm.ChangeState<PlayerIdleState>();
        }
    }
}