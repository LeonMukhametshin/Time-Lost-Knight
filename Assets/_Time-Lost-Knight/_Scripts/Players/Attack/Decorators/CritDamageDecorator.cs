using Attacks;
using System;

public class CritDamageDecorator : WeaponDecorator
{
    protected CritConfig m_critConfig;
    protected IWeapon m_weapon;

    private Random random = new();

    public CritDamageDecorator(IWeapon weapon, CritConfig critConfig) : base(weapon)
    {
        m_critConfig = critConfig;
    }

    public override void ApplyDamage(ICanBeDamageable damageable)
    { 
        int totalDamage = CalculateDamage();
        damageable.TakeDamage(totalDamage);
    }

    public override int CalculateDamage()
    {
        if (random.NextDouble() <= m_critConfig.critChance)
        {
            var critDamage = m_critConfig.critMultiplier * m_critConfig.weaponConfig.baseDamage;
            return (int)Math.Ceiling(critDamage);
        }
        return base.CalculateDamage();
    }
}