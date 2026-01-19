using UnityEngine;

[CreateAssetMenu(fileName = "MeleeWeaponData", menuName = "Scriptable Objects/MeleeWeaponData")]
public class MeleeWeaponData : WeaponConfig
{
    [field: SerializeField] public Vector2 attackZone { get; private set; }
}
