public class PlayerCrouchMoveState : PlayerGroundState
{
    protected FlipContoller flipController
    {
        get => m_flipContoller ??= core.GetCoreComponent<FlipContoller>();
    }
    protected ColliderController colliderController
    {
        get => m_colliderController ??= core.GetCoreComponent<ColliderController>();
    }

    private FlipContoller m_flipContoller;
    private ColliderController m_colliderController;

    public PlayerCrouchMoveState(Player player, PlayerFSM fsm, 
        PlayerData playerData, string animBoolName) 
        : base(player, fsm, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        colliderController.SetColliderHeight(data.crouchColliderHeight);
    }

    public override void Exit()
    {
        base.Exit();
        colliderController.SetColliderHeight(data.standColliderHeight);
    }

    public override void Update()
    {
        base.Update();

        if (isExitingState)
        {
            return;
        }

        movement.SetVelocityX(data.crouchMovementVelocity * flipController.facingDirection);
        flipController.CheckIfShoudFlip(xInput);

        if (xInput == 0)
        {
            fsm.SetState(player.statesContainer.GetState<PlayerCrouchIdleState>());
        }
        else if (yInput != -1 && !isTouchingCeiling)
        {
            fsm.SetState(player.statesContainer.GetState<PlayerMoveState>());
        }
    }
}
