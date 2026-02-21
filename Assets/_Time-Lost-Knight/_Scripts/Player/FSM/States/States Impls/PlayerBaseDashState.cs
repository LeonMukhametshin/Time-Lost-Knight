using UnityEngine;

public class PlayerBaseDashState : PlayerAbilytiState
{
    protected bool m_isHolding;
    protected bool m_dashInputStop;

    protected Vector2 m_dashDirection;
    protected Vector2 m_dashDirectionInput;
    protected Vector2 m_lastAfterImagePosition;

    public PlayerBaseDashState(EntityFSM fsm, Core core,
        string animBoolName, Player player,
        PlayerData data)
        : base(fsm, core, animBoolName, player, data)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.inputHandler.UseDashInput();
        m_isHolding = true;
    }
}