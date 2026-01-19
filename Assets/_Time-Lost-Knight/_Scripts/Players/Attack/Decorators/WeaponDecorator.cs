namespace Attacks
{
    public abstract class WeaponDecorator : IWeapon
    {
        protected IWeapon weapon;

        public WeaponDecorator(IWeapon weapon)
        {
            this.weapon = weapon;
        }

        public WeaponConfig config { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public virtual void ApplyDamage(ICanBeDamageable damageable) =>
            weapon.ApplyDamage(damageable);

        public virtual int CalculateDamage() =>
            weapon.CalculateDamage();
    }
}