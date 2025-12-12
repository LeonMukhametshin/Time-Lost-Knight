using UnityEngine;

namespace Attacks
{
    public abstract class WeaponConfig : ScriptableObject
    {
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public float Cooldown { get; private set; }
    }
}