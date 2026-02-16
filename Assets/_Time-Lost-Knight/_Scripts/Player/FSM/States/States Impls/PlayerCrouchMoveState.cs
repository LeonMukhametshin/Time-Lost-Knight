public class PlayerCrouchMoveState : PlayerGroundState
{
    public PlayerCrouchMoveState(Player player, PlayerFSM fsm, 
        PlayerData playerData, string animBoolName) 
        : base(player, fsm, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.colliderController.SetColliderHeight(data.crouchColliderHeight);
    }

    public override void Exit()
    {
        base.Exit();
        player.colliderController.SetColliderHeight(data.standColliderHeight);
    }

    public override void Update()
    {
        base.Update();

        if (isExitingState)
        {
            return;
        }

        core.movement.SetVelocityX(data.crouchMovementVelocity * core.flipController.facingDirection);
        core.flipController.CheckIfShoudFlip(xInput);

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
