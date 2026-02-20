using UnityEngine;

public abstract class Combat : CoreComponent, IDamageable
{
    [SerializeField] private HealthSystem m_healthSystem;

    public override void Awake()
    {
        base.Awake();
        //TODO: remove 
        m_healthSystem.Initialize(100);
    }

    public virtual void TakeDamage(float amount)
    {
        m_healthSystem.Decrease(amount);
      
    }
}