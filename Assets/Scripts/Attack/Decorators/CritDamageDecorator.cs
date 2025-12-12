using Attacks;

public class CritDamageDecorator : WeaponDecorator
{
    protected CritWeaponConfig m_critConfig;
    protected IDamageCalculator m_damageCalculator;

    public CritDamageDecorator(IWeapon weapon, IDamageCalculator damageCalculator, CritWeaponConfig critConfig) : base(weapon)
    {
        m_damageCalculator = damageCalculator;
        m_critConfig = critConfig;
    }

    public override void ApplyDamage(ICanBeDamageable damageable)
    {
        int damage = m_damageCalculator.Calculate(m_critConfig);
        damageable.TakeDamage(MainWeapon.GetDamageType() ,damage);
    }
}