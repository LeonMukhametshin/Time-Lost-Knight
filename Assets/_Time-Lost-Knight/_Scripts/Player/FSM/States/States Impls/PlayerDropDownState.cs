using UnityEngine;

public class PlayerDropDownState : PlayerState
{
    private Collider2D m_platformCollider;
    private bool m_isIgnoringCollision;

    public PlayerDropDownState(Player player, PlayerFSM fsm,
        PlayerData playerData, string animBoolName)
        : base(player, fsm, playerData, animBoolName)
    {
    }

    public void SetPlatformCollider(Collider2D platformCollider) =>
        m_platformCollider = platformCollider;

    public override void Enter()
    {
        base.Enter();

        player.inputHandler.UseJumpInput();

        if (m_platformCollider != null && player.bodyCollider != null)
        {
            Physics2D.IgnoreCollision(player.bodyCollider, m_platformCollider, true);
            m_isIgnoringCollision = true;
        }

        player.movement.SetVelocityY(-data.dropVelocity);
    }

    public override void Update()
    {
        base.Update();

        if (Time.time >= startTime + data.dropThroughDuration)
        {
            fsm.SetState(player.statesContainer.GetState<PlayerInAirState>());
        }
    }

    public override void Exit()
    {
        if (m_isIgnoringCollision && m_platformCollider != null && player.bodyCollider != null)
        {
            Physics2D.IgnoreCollision(player.bodyCollider, m_platformCollider, false);
        }

        m_platformCollider = null;
        m_isIgnoringCollision = false;

        base.Exit();
    }
}
