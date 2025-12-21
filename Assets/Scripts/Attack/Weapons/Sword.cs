namespace Attacks
{
    public class Sword : Weapon
    {
        public Sword(MeleeWeaponConfig config) : base(config)
        {
            m_config = config;
        }

        public override void ApplyDamage(ICanBeDamageable damageable) => 
            base.ApplyDamage(damageable);

        public override int CalculateDamage() =>
            base.CalculateDamage();
    }
}