public abstract class Combat : CoreComponent, IEffectable, IDamageable
{
    private HealthComponent m_healthComponent;
    protected HealthComponent healthComponent =>
        m_healthComponent ??= core.GetCoreComponent<HealthComponent>();

    public virtual void TakeDamage(float amount)
    {
        healthComponent?.TakeDamage(amount);
    }
}