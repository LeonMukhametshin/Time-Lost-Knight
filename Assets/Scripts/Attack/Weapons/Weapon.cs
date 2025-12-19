namespace Attacks
{
    public class Weapon : IWeapon
    {
        protected WeaponConfig m_config;

        public Weapon(WeaponConfig config)
        {
            m_config = config;
        }

        public virtual void ApplyDamage(ICanBeDamageable damageable)
        {
            var totalDamage = CalculateDamage();
            damageable.TakeDamage(totalDamage);
        }

        public virtual int CalculateDamage() =>
            m_config.baseDamage;
    }
}