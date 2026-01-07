using UnityEngine;

namespace Attacks
{
    [CreateAssetMenu(fileName = "WeaponConfig", menuName = "Scriptable Objects/WeaponConfig")]
    public sealed class MeleeWeaponConfig : WeaponConfig
    {
        [field: SerializeField] public float KnockbackForce { get; private set; }
        [field: SerializeField] public Vector2 AttackZone { get; private set; }
    }
}