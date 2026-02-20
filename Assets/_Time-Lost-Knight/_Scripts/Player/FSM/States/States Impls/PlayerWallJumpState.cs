using UnityEngine;

public class PlayerWallJumpState : PlayerAbilytiState
{
    private int m_wallJumpDirection;

    public PlayerWallJumpState(EntityFSM fsm, Core core, 
        string animBoolName, Player player,
        PlayerData data) 
        : base(fsm, core, animBoolName, player, data)
    {
    }

    public override void Enter()
    {
        base.Enter();

        var jumpState = fsm.GetState<PlayerJumpState>();
        player.inputHandler.UseJumpInput();
        jumpState.ResetAmountOfJumpsLeft();
        movement.SetVelocity(data.wallJumpVelocity, data.wallJumpAnge, m_wallJumpDirection);
        flipController.CheckIfShoudFlip(m_wallJumpDirection);
        jumpState.DecreaseAmountOfJumpLeft();
    }

    public override void Update()
    {
        base.Update();

        player.animationController.animator
            .SetFloat(PlayerAnimationConstants.Y_VELOCITY, movement.currentVelocity.y);
        player.animationController.animator
            .SetFloat(PlayerAnimationConstants.X_VELOCITY, Mathf.Abs(movement.currentVelocity.x));

        if(Time.time >= startTime + data.wallJumpTime)
        {
            isAbilityDone = true;
        }
    }

    public void DetermineWallJumpDirection(bool isTouchingWall)
    {
        m_wallJumpDirection = isTouchingWall
            ? -flipController.facingDirection
            : flipController.facingDirection;
    }
}