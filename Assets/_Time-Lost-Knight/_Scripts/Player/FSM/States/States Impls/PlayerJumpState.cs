public class PlayerJumpState : PlayerAbilytiState
{
    private int amountOfJumpsLeft;

    public PlayerJumpState(Player player, PlayerFSM fsm, 
        PlayerData playerData, string animBoolName) 
        : base(player, fsm, playerData, animBoolName)
    {
        amountOfJumpsLeft = playerData.amountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();

        player.inputHandler.UseJumpInput();
        player.movement.SetVelocityY(data.jumpVelocity); 
        isAbilityDone = true;

        DecreaseAmountOfJumpLeft();
        player.statesContainer.GetState<PlayerInAirState>().SetIsJumping();
    }

    public bool CanJump() =>
        amountOfJumpsLeft > 0;

    public void ResetAmountOfJumpsLeft() =>
        amountOfJumpsLeft = data.amountOfJumps;

    public void DecreaseAmountOfJumpLeft() =>
        amountOfJumpsLeft--;
}