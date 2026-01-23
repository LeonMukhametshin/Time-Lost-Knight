using UnityEngine;

[CreateAssetMenu(fileName = "MeleeWeaponData", menuName = "Scriptable Objects/Weapon/MeleeWeaponData")]
public class MeleeWeaponData : WeaponConfig
{
    [field: SerializeField] public Vector2 attackZone { get; private set; }
}
