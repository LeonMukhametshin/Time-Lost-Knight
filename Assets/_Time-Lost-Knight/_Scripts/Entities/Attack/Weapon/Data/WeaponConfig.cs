using UnityEngine;

public abstract class WeaponConfig : ScriptableObject
{
    [Header("Main")]
    [field: SerializeField] public new string name { get; private set; }
    [field: SerializeField] public GameObject view { get; private set; }
    [field: SerializeField] public WeaponType weaponType { get; private set; }

    [Header("Effects")]
    [SerializeReference]
    [SerializeReferenceDropdown]
    [SerializeField] private IEffect[] m_effects;

    public IEffect[] effects => m_effects;

    [Header("Timers")]
    [field: SerializeField] public float windupTime { get; private set; }
    [field: SerializeField] public float cooldown { get; private set; }

    public override string ToString() =>
        $"{name} {weaponType}";
}