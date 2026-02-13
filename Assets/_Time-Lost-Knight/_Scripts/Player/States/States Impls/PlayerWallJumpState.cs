using UnityEngine;

public class PlayerWallJumpState : PlayerAbilytiState
{
    private int m_wallJumpDirection;

    public PlayerWallJumpState(Player player, PlayerFSM fsm, PlayerData playerData, string animBoolName) 
        : base(player, fsm, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.inputHandler.UseJumpInput();
        player.statesContainer.jumpState.ResetAmountOfJumpsLeft();
        player.movement.SetVelocity(data.wallJumpVelocity, data.wallJumpAnge, m_wallJumpDirection);
        player.flipController.CheckIfShoudFlip(m_wallJumpDirection);
        player.statesContainer.jumpState.DecreaseAmountOfJumpLeft();
    }

    public override void Update()
    {
        base.Update();

        player.animationController.animator.SetFloat(PlayerAnimationConst.Y_VELOCITY, player.movement.currentVelocity.y);
        player.animationController.animator.SetFloat(PlayerAnimationConst.X_VELOCITY, Mathf.Abs(player.movement.currentVelocity.x));

        if(Time.time >= startTime + data.wallJumpTime)
        {
            isAbilityDone = true;
        }
    }

    public void DetermineWallJumpDirection(bool isTouchingWall)
    {
        m_wallJumpDirection = isTouchingWall
            ? -player.collisionDetector.facingDirection
            : player.collisionDetector.facingDirection;
    }
}