using Attacks;
using UnityEngine;

public class WeaponFactory : MonoBehaviour
{
    public static IWeapon CreateBaseWeapon(WeaponConfig config) =>
        new Weapon(config);

    public static IWeapon AddCritical(
        IWeapon weapon,
        CritConfig critConfig)
        => new CritDamageDecorator(weapon, critConfig);
}