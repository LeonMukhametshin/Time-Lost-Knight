using System;
using UnityEngine;

[Serializable]
public class HealthBuff : BaseBuff
{
    [SerializeField][Min(0)] private float m_value;

    private IHealth m_health;

    public HealthBuff() { }

    public HealthBuff(
        string id, 
        Sprite icon, 
        BuffType type,
        float heal) 
        : base(id, icon, type)
    {
        m_value = heal;
    }

    protected override void OnInitialize()
    {
        base.OnInitialize();

        m_health = container.core.GetCoreComponent<HealthComponent>();

        if(m_health is null)
        {
            Deinitialize();
            return;
        }

        m_health.Heal(m_value);
    }

    public override IBuff Clone() =>
        new HealthBuff(id, icon, type, m_value);
}