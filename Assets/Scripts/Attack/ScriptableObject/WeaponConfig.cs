using UnityEngine;

namespace Attacks
{
    public abstract class WeaponConfig : ScriptableObject
    {
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public float Cooldown { get; private set; }
        [field: SerializeField][Range(0, 100)] public float CriticalChance { get; private set; }
        [field: SerializeField][Range(0, 2)] public float CriticalMultiplier { get; private set; }
    }
}