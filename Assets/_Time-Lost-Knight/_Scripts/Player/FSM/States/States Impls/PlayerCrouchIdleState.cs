public class PlayerCrouchIdleState : PlayerGroundState
{
    public PlayerCrouchIdleState(EntityFSM fsm, Core core, 
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
            fsm.ChangeState<PlayerCrouchMoveState>();
        }
        else if (yInput != -1 && !isTouchingCeiling)
        {
            fsm.ChangeState<PlayerIdleState>();
        }
    }
}