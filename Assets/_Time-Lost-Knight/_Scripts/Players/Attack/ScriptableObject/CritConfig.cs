using UnityEngine;

namespace Attacks
{
    [CreateAssetMenu(fileName = "CritConfig", menuName = "Scriptable Objects/CritConfig")]
    public class CritConfig : ScriptableObject
    {
        public WeaponConfig weaponConfig { get; private set; }
        [field: SerializeField, Range(0f, 1f)] public float critChance { get; private set; }
        [field: SerializeField, Range(0f, 2f)] public float critMultiplier { get; private set; }

        public void Initialize(WeaponConfig config) =>
            weaponConfig = config;
    }
}