public class PlayerCrouchMoveState : PlayerGroundState
{
    protected FlipContoller flipController => 
        m_flipContoller ??= core.GetCoreComponent<FlipContoller>();

    private FlipContoller m_flipContoller;

    public PlayerCrouchMoveState(EntityFSM fsm, Core core, 
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
        movement.SetVelocityXSmooth(data.crouchMovementVelocity * xInput, data.movementAcceleration, data.movementDeceleration);
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
