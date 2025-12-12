using Attacks;

public class CriticalDamageDecorator : WeaponDecorator
{
    protected WeaponConfig m_critConfig;
    protected IDamageCalculator m_damageCalculator;

    public CriticalDamageDecorator(IWeapon weapon, IDamageCalculator damageCalculator, WeaponConfig critConfig) : base(weapon)
    {
        m_damageCalculator = damageCalculator;
        m_critConfig = critConfig;
    }

    public override void ApplyDamage(ICanBeDamageable damageable) =>
        damageable.TakeDamage(
            MainWeapon.GetDamageType(),
            m_damageCalculator.Calculate(m_critConfig)
            );
}