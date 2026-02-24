using UnityEngine;

public class PlayerRangedAttackState : PlayerAbilytiState, IAnimationTrigger
{
    private readonly Transform m_attackPosition;
    private readonly RangeAttackData m_data;

    /// <summary>
    /// Если анимация не вызывает FinishAnimation (нет клипа/событий),
    /// стейт всё равно выйдет через это время. Задаётся в RangeAttackData или здесь по умолчанию.
    /// </summary>
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
        //movement.SetVelocityX(0f);
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

    public void TriggerAnimation()
    {
        if (m_data == null || m_attackPosition == null)
            return;
        GameObject go = Object.Instantiate(m_data.projectile, m_attackPosition.position, m_attackPosition.rotation);
        if (go.TryGetComponent(out PlayerProjectile playerProj))
        {
            playerProj.Initialize(m_data);
        }
        else if (go.TryGetComponent(out ExplosiveProjectile explosive))
        {
            explosive.Initialize(m_data);
        }
        else if (go.TryGetComponent(out Projectile projectileScript))
        {
            projectileScript.Initialize(m_data);
        }
    }

    public void FinishAnimation()
    {
        isAbilityDone = true;
    }
}
