using Attacks;

public class CalculateBace : IDamageCalculator
{
    public int Calculate(WeaponConfig weaponConfig) => weaponConfig.Damage;
}