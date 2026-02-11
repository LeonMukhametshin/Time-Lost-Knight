using UnityEngine;

public class PlayerCrouchMoveState : PlayerGroundState
{
    public PlayerCrouchMoveState(Player player, PlayerFSM fsm, PlayerData playerData, string animBoolName) 
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

        if (!isExitingState)
        {
            player.movement.SetVelocityX(data.crouchMovementVelocity * player.collisionDetector.facingDirection);
            player.flipController.CheckIfShoudFlip(xInput);

            if (xInput == 0)
            {
                fsm.SetState(player.statesContainer.crouchIdleState);
            }
            else if(yInput != -1 && !isTouchingCeiling)
            {
                fsm.SetState(player.statesContainer.moveState);
            }
        }
    }
}
