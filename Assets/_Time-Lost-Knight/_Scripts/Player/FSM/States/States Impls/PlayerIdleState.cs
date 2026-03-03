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
        movement.SetVelocityX(0f);
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
        else if (yInput == -1 &&
                 player.abilities != null &&
                 player.abilities.IsEnabled(PlayerAbilityType.Crouch))
        {
            fsm.ChangeState<PlayerCrouchIdleState>();
        }
    }
}