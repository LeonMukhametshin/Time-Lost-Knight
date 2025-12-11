public abstract class WeaponDecorator : IWeapon
{
    protected IWeapon MainWeapon;
    protected DamageType m_damageType;

    public WeaponDecorator(IWeapon weapon, DamageType type)
    {
        MainWeapon = weapon;
        m_damageType = type;
    }

    public virtual void ApplyDamage(ICanBeDamageable damageable)
    {
        MainWeapon.ApplyDamage(damageable);
    }

    public virtual DamageType GetDamageType() => MainWeapon.GetDamageType();

    public virtual int GetDamage() => MainWeapon.GetDamage();
}