public class Weapon : IWeapon
{
    private int m_damage;
    private DamageType m_damageType;

    public Weapon(int damage, DamageType damageType)
    {
        m_damage = damage;
        m_damageType = damageType;
    }

    public void ApplyDamage(ICanBeDamageable damageable)
    {
        damageable.TakeDamage(m_damageType, m_damage);
    }

    public int GetDamage() => m_damage;

    public DamageType GetDamageType() => m_damageType;
}