public class PlayerCrouchMoveState : PlayerGroundState
{
    protected FlipContoller flipController => 
        m_flipContoller ??= core.GetCoreComponent<FlipContoller>();

    protected ColliderController colliderController => 
        m_colliderController ??= core.GetCoreComponent<ColliderController>();

    private FlipContoller m_flipContoller;
    private ColliderController m_colliderController;

    public PlayerCrouchMoveState(EntityFSM fsm, Core core, 
        string animBoolName, Player player, 
        PlayerData data) 
        : base(fsm, core, animBoolName, player, data)
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

        movement.SetVelocityX(data.crouchMovementVelocity * xInput);
        flipController.CheckIfShoudFlip(xInput);

        if (xInput == 0)
        {
            fsm.ChangeState<PlayerCrouchIdleState>();
        }
        else if (yInput != -1 && !isTouchingCeiling)
        {
            fsm.ChangeState<PlayerMoveState>();
        }
    }
}
