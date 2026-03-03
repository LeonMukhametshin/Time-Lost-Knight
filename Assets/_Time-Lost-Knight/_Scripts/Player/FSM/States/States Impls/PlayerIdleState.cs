public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(EntityFSM fsm, Core core, 
        string animBoolName, Player player, 
        PlayerData data) 
        : base(fsm, core, animBoolName, player, data)
    {
    }

    public override void Enter()
    {
        base.Enter();
        movement.SetVelocityXSmooth(0f, data.movementAcceleration, data.movementDeceleration);
    }

    public override void Update()
    {
        base.Update();

        if (isExitingState)
        {
            return; 
        }

        movement.SetVelocityXSmooth(0f, data.movementAcceleration, data.movementDeceleration);

        if (xInput != 0)
        {
            fsm.ChangeState<PlayerMoveState>();
        }
        else if (yInput == -1)
        {
            fsm.ChangeState<PlayerCrouchIdleState>();
        }
    }
}