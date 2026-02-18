using UnityEngine;

public class PlayerWallJumpState : PlayerAbilytiState
{
    protected FlipContoller flipController
    {
        get => m_flipContoller ??= core.GetCoreComponent<FlipContoller>();
    }
    private FlipContoller m_flipContoller;

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
        movement.SetVelocity(data.wallJumpVelocity, data.wallJumpAnge, m_wallJumpDirection);
        flipController.CheckIfShoudFlip(m_wallJumpDirection);
        jumpState.DecreaseAmountOfJumpLeft();
    }

    public override void Update()
    {
        base.Update();

        player.animationController.animator
            .SetFloat(PlayerAnimationÑonstants.Y_VELOCITY, movement.currentVelocity.y);
        player.animationController.animator
            .SetFloat(PlayerAnimationÑonstants.X_VELOCITY, Mathf.Abs(movement.currentVelocity.x));

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