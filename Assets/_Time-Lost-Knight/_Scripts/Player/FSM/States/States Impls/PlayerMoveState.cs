public class PlayerMoveState : PlayerGroundState
{
    protected FlipContoller flipController
    {
        get => m_flipContoller ??= core.GetCoreComponent<FlipContoller>();
    }

    private FlipContoller m_flipContoller;

    public PlayerMoveState(Player player, EntityFSM fsm,
        PlayerData playerData, string animBoolName) 
        : base(player, fsm, playerData, animBoolName)
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
            fsm.SetState(player.statesContainer.GetState<PlayerIdleState>());
        }
        else if (yInput == -1)
        {
            fsm.SetState(player.statesContainer.GetState<PlayerCrouchIdleState>());
        }
    }
}