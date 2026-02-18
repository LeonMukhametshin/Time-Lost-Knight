public class PlayerCrouchIdleState : PlayerGroundState
{
    protected ColliderController colliderController
    {
        get => m_colliderController ??= core.GetCoreComponent<ColliderController>();
    }

    private ColliderController m_colliderController;

    public PlayerCrouchIdleState(Player player, PlayerFSM fsm, 
        PlayerData playerData, string animBoolName)
        : base(player, fsm, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        movement.SetVelocityZero();
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

        if (xInput != 0)
        {
            fsm.SetState(player.statesContainer.GetState<PlayerCrouchMoveState>());
        }
        else if (yInput != -1 && !isTouchingCeiling)
        {
            fsm.SetState(player.statesContainer.GetState<PlayerIdleState>());
        }
    }
}