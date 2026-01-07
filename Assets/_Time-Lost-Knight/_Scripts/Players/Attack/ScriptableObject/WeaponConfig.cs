using UnityEngine;

namespace Attacks
{
    public abstract class WeaponConfig : ScriptableObject
    {
        [field: SerializeField] public int baseDamage { get; private set; }
        [field: SerializeField] public float cooldown { get; private set; }
        [field: SerializeField] public DamageType damageType { get; private set; }
    }
}