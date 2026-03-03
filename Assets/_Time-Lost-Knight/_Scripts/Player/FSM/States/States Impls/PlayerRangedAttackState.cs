using UnityEngine;

public class PlayerRangedAttackState : PlayerAbilytiState, IAnimationTrigger
{
    private readonly Transform m_attackPosition;
    private readonly RangeAttackData m_data;

    private const float FallbackExitTime = 0.5f;

    private float m_enterTime;

    public PlayerRangedAttackState(EntityFSM fsm, Core core,
        string animBoolName, Player player,
        PlayerData playerData,
        Transform attackPosition, RangeAttackData rangedData)
        : base(fsm, core, animBoolName, player, playerData)
    {
        m_attackPosition = attackPosition;
        m_data = rangedData;
    }

    public override void Enter()
    {
        base.Enter();

        m_enterTime = Time.time;
        player.inputHandler.UseRangedAttackInput();
        TriggerAnimation();
    }

    public override void Update()
    {
        base.Update();

        if (!isAbilityDone && Time.time - m_enterTime >= FallbackExitTime)
        {
            isAbilityDone = true;
        }
    }

    public override void TriggerAnimation()
    {
        if (m_data == null || m_attackPosition == null)
            return;
        var projectileInstance = Object.Instantiate(m_data.projectile, m_attackPosition.position, m_attackPosition.rotation);
        if (projectileInstance.TryGetComponent(out BaseProjectile projectile))
        {
            projectile.Initialize(m_data);
        }
    }

    public override void FinishAnimation()
    {
        isAbilityDone = true;
    }
}
