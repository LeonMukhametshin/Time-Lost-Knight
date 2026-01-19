using Attacks;

public interface IWeapon
{
    public WeaponConfig config { get; }

    void ApplyDamage(ICanBeDamageable damageable);
    int CalculateDamage();
}