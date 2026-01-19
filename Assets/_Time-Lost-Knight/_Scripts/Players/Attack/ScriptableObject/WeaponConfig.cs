using UnityEngine;

namespace Attacks
{
    public abstract class WeaponConfig : ScriptableObject
    {
        [field: SerializeField] public string name { get; private set; }
        [field: SerializeField] public int baseDamage { get; private set; }
        [field: SerializeField] public float windupTime { get; private set; }
        [field: SerializeField] public float cooldown { get; private set; }
        [field: SerializeField] public DamageType damageType { get; private set; }

        public override string ToString() =>
            $"{name} Damage: {baseDamage}, windupTime: {windupTime}, cooldown: {cooldown}," +
                $" damageType: {damageType}";
    }
}