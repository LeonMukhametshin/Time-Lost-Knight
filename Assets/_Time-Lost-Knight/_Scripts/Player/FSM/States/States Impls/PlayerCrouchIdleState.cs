public class PlayerCrouchIdleState : PlayerGroundState
{
    protected ColliderController colliderController =>
        m_colliderController ??= core.GetCoreComponent<ColliderController>();

    private ColliderController m_colliderController;

    public PlayerCrouchIdleState(EntityFSM fsm, Core core, 
        string animBoolName, Player player, 
        PlayerData data) 
        : base(fsm, core, animBoolName, player, data)
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
            fsm.ChangeState<PlayerCrouchMoveState>();
        }
        else if (yInput != -1 && !isTouchingCeiling)
        {
            fsm.ChangeState<PlayerIdleState>();
        }
    }
}