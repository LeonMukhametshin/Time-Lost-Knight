using UnityEngine;

public abstract class Combat : CoreComponent, IDamageable
{
    [SerializeField] private Entity m_entity;

    public override void Awake()
    {
        base.Awake();
    }

    public virtual void TakeDamage(float amount)
    {
        m_entity.healthSystem.TakeDamage(amount);
    }
}