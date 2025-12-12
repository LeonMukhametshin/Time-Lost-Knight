namespace Attacks
{
    public class Weapon : IWeapon
    {
        protected WeaponConfig m_config;

        private DamageType m_damageType;

        public Weapon(WeaponConfig config, DamageType damageType)
        {
            m_config = config;
            m_damageType = damageType;
        }

        public void ApplyDamage(ICanBeDamageable damageable)
        {
            damageable.TakeDamage(m_damageType, m_config.Damage);
        }

        public int GetDamage() => m_config.Damage;

        public DamageType GetDamageType() => m_damageType;
    }
}