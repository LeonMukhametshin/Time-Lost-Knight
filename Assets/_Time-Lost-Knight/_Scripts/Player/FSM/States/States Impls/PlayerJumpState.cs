public class PlayerJumpState : PlayerAbilytiState
{
    private int amountOfJumpsLeft;

    public PlayerJumpState(EntityFSM fsm, Core core, 
        string animBoolName, Player player, 
        PlayerData data) 
        : base(fsm, core, animBoolName, player, data)
    {
        amountOfJumpsLeft = data.amountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();

        player.inputHandler.UseJumpInput();
        movement.SetVelocityY(data.jumpVelocity); 
        isAbilityDone = true;

        DecreaseAmountOfJumpLeft();
        fsm.GetState<PlayerAirState>().SetIsJumping();
    }

    public bool CanJump() =>
        amountOfJumpsLeft > 0;

    public void ResetAmountOfJumpsLeft() =>
        amountOfJumpsLeft = data.amountOfJumps;

    public void DecreaseAmountOfJumpLeft() =>
        amountOfJumpsLeft--;
}