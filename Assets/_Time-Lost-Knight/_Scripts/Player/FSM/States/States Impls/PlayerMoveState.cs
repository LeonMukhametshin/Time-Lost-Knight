public class PlayerMoveState : PlayerGroundState
{
    protected FlipContoller flipController => 
        m_flipContoller ??= core.GetCoreComponent<FlipContoller>();

    private FlipContoller m_flipContoller;

    public PlayerMoveState(EntityFSM fsm, Core core,
        string animBoolName, Player player, 
        PlayerData data) 
        : base(fsm, core, animBoolName, player, data)
    {
    }

    public override void Update()
    {
        base.Update();

        movement.SetVelocityX(data.movementSpeed * xInput);
        flipController.CheckIfShoudFlip(xInput);

        if(isExitingState)
        {
            return;
        }

        if (xInput == 0)
        {
            fsm.ChangeState<PlayerIdleState>();
        }
        else if (yInput == -1 &&
                 player.abilities != null &&
                 player.abilities.IsEnabled(PlayerAbilityType.Crouch))
        {
            fsm.ChangeState<PlayerCrouchIdleState>();
        }
    }
}