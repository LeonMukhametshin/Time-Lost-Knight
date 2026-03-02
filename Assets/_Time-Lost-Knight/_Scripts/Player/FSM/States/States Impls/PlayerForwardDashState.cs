using UnityEngine;

public class PlayerForwardDashState : PlayerBaseDashState
{
    protected override bool canHoldDirection => false;
    protected override bool showDashVisualizer => false;

    private Vector2 m_workspace;

    public PlayerForwardDashState(EntityFSM fsm, Core core,
        string animBoolName, Player player,
        PlayerData data)
        : base(fsm, core, animBoolName, player, data)
    {
    }

    protected override Vector2 ResolveDashDirection(Vector2 fallbackDirection)
    {
        m_workspace.Set(fallbackDirection.x, 0f);
        return m_workspace.normalized;
    }
}