using Attacks;
using UnityEngine;

public class WeaponFactory : MonoBehaviour
{
    public static IWeapon CreateBaseWeapon(WeaponConfig config) =>
        new Weapon(config, DamageType.Physical);

    public static IWeapon AddCritical(
        IWeapon weapon,
        CritWeaponConfig critConfig,
        IDamageCalculator calculator)
        => new CritDamageDecorator(weapon, calculator, critConfig);
}