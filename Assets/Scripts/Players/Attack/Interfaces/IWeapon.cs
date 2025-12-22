public interface IWeapon
{
    void ApplyDamage(ICanBeDamageable damageable);
    int CalculateDamage();
}