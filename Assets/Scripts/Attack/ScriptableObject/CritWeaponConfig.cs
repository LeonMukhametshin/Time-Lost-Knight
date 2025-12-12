using UnityEngine;

namespace Attacks
{
    [CreateAssetMenu(fileName = "CritConfig", menuName = "Scriptable Objects/CritConfig")]
    public class CritWeaponConfig : WeaponConfig
    {
        [field: SerializeField][Range(0, 100)] public float CriticalChance { get; private set; }
        [field: SerializeField][Range(0, 2)] public float CriticalMultiplier { get; private set; }
    }
}