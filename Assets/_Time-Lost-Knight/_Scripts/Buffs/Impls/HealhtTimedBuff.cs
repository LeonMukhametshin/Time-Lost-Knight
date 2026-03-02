using System;
using UnityEngine;

public class HealhtTimedBuff : TimeBuff
{

    [SerializeField][Min(0)] private float m_interval = 1f;
    [SerializeField][Min(0)] private float m_healthPerIntercal = 1f;

    [NonSerialized] private float m_timer;
    private IHealth m_health;

    public HealhtTimedBuff(
        string id, 
        Sprite icon, 
        BuffType type, 
        float duration,
        float intercal,
        float healthPerIntercal) 
        : base(id, icon, type, duration)
    {
        m_interval = intercal;
        m_healthPerIntercal = healthPerIntercal;
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
            m_health.Heal(m_healthPerIntercal);
        }
    }

    public override IBuff Clone() =>
        new HealhtTimedBuff(id, icon, type, 
            duration, m_interval, m_healthPerIntercal);
}