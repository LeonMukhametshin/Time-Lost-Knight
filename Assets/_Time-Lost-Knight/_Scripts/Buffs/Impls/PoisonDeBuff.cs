using System;
using UnityEngine;

[Serializable]
public sealed class PoisonDeBuff : TimeBuff
{
    [SerializeField][Min(0)] private float m_interval = 1f;
    [SerializeField][Min(0)] private float m_damagePerSeconds = 2f;

    [NonSerialized] private float m_timer;
    private IHealth m_health;

    public PoisonDeBuff(
        string id,
        Sprite sprite,
        BuffType type,
        float duration,
        float interval,
        float damagePerSeconds) 
        : base(id, sprite, type, duration)
    {
        m_interval = interval;
        m_damagePerSeconds = damagePerSeconds;
    }

    protected override void OnInitialize()
    {
        base.OnInitialize();
        m_health = container.core.GetCoreComponent<HealthComponent>();
    }

    protected override void OnDeinitializing()
    {
        m_timer = 0;
        m_health = null;
        base.OnDeinitializing();
    }

    protected override void OnUpdate(float deltaTime)
    {
        if (m_health is null)
        {
            Deinitialize();
            return;
        }

        if (m_timer < m_interval)
        {
            m_timer += deltaTime;
        }
        else
        {
            m_timer = 0f;
            m_health.TakeDamage(m_damagePerSeconds);
        }
    }

    public override IBuff Clone() =>
        new PoisonDeBuff(id, icon, type, duration, m_interval, m_damagePerSeconds);
}