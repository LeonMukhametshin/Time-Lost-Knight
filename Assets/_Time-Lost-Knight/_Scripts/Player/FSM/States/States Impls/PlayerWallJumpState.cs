using UnityEngine;

public class PlayerWallJumpState : PlayerAbilytiState
{
    private int m_wallJumpDirection;

    public PlayerWallJumpState(Player player, PlayerFSM fsm,
        PlayerData playerData, string animBoolName) 
        : base(player, fsm, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        var jumpState = player.statesContainer.GetState<PlayerJumpState>();
        player.inputHandler.UseJumpInput();
        jumpState.ResetAmountOfJumpsLeft();
        core.movement.SetVelocity(data.wallJumpVelocity, data.wallJumpAnge, m_wallJumpDirection);
        core.flipController.CheckIfShoudFlip(m_wallJumpDirection);
        jumpState.DecreaseAmountOfJumpLeft();
    }

    public override void Update()
    {
        base.Update();

        player.animationController.animator
            .SetFloat(PlayerAnimationÑonstants.Y_VELOCITY, core.movement.currentVelocity.y);
        player.animationController.animator
            .SetFloat(PlayerAnimationÑonstants.X_VELOCITY, Mathf.Abs(core.movement.currentVelocity.x));

        if(Time.time >= startTime + data.wallJumpTime)
        {
            isAbilityDone = true;
        }
    }

    public void DetermineWallJumpDirection(bool isTouchingWall)
    {
        m_wallJumpDirection = isTouchingWall
            ? -core.collisionDetector.facingDirection
            : core.collisionDetector.facingDirection;
    }
}