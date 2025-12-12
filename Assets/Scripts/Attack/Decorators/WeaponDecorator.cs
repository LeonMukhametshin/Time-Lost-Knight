namespace Attacks
{
    public abstract class WeaponDecorator : IWeapon
    {
        protected IWeapon MainWeapon;

        public WeaponDecorator(IWeapon weapon)
        {
            MainWeapon = weapon;
        }

        public virtual void ApplyDamage(ICanBeDamageable damageable)
        {
            MainWeapon.ApplyDamage(damageable);
        }

        public virtual DamageType GetDamageType() => MainWeapon.GetDamageType();

        public virtual int GetDamage() => MainWeapon.GetDamage();
    }
}