public interface IWeapon
{
    DamageType GetDamageType();
    int GetDamage();
    void ApplyDamage(ICanBeDamageable damageable);
}