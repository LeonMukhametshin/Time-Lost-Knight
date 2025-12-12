namespace Attacks
{
    public class Sword : Weapon
    {
        private MeleeWeaponConfig m_config;
        private IDamageCalculator m_damageCalculator;

        public Sword(MeleeWeaponConfig config, DamageType damageType, IDamageCalculator damageCalculator) : base(config, damageType)
        {
            m_config = config;
            m_damageCalculator = damageCalculator;
        }

        public virtual void ApplyDamage(ICanBeDamageable damageable)
        {
            damageable.TakeDamage(GetDamageType(), m_damageCalculator.Calculate(m_config));
        }
    }
}