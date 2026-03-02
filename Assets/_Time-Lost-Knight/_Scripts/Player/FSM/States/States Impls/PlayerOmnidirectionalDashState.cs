using UnityEngine;

public class PlayerOmnidirectionalDashState : PlayerBaseDashState
{
    protected override bool canHoldDirection => true;
    protected override bool showDashVisualizer => true;

    public PlayerOmnidirectionalDashState(EntityFSM fsm, Core core,
        string animBoolName, Player player,
        PlayerData data)
        : base(fsm, core, animBoolName, player, data)
    {
    }

    protected override Vector2 ResolveDashDirection(Vector2 fallbackDirection)
    {
        Vector2 inputDirection = player.inputHandler.dashDirectionInput;

        if (inputDirection != Vector2.zero)
        {
            return inputDirection.normalized;
        }

        return fallbackDirection;
    }
}