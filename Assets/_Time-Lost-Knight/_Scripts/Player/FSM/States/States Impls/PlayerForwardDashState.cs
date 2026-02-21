using UnityEngine;

public class PlayerForwardDashState : PlayerBaseDashState
{
    protected override bool CanHoldDirection => false;
    protected override bool ShowDashVisualizer => false;

    public PlayerForwardDashState(EntityFSM fsm, Core core,
        string animBoolName, Player player,
        PlayerData data)
        : base(fsm, core, animBoolName, player, data)
    {
    }

    protected override Vector2 ResolveDashDirection(Vector2 fallbackDirection)
    {
        Vector2 inputDirection = player.inputHandler.rawMovementInput;

        if (inputDirection.sqrMagnitude > 0f)
        {
            return inputDirection.normalized;
        }

        return fallbackDirection;
    }
}