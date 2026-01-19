using Attacks;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    [SerializeField] private MeleeWeaponConfig[] weaponConfigs;
    [SerializeField] private CoroutineRunner m_runner;

    private Dictionary<AttackSlot, WeaponSlot> m_slots;
    private AttackCaster m_caster;

    private void Awake()
    {
        m_caster = new AttackCaster();

        m_slots = new Dictionary<AttackSlot, WeaponSlot>
        {
            {AttackSlot.Main, new WeaponSlot(new Sword(weaponConfigs[0]), m_caster, m_runner) },
            {AttackSlot.Additional, new WeaponSlot(new Sword(weaponConfigs[1]), m_caster, m_runner) },
            {AttackSlot.AbilityE, new WeaponSlot(new Sword(weaponConfigs[2]), m_caster, m_runner) },
            {AttackSlot.AbilityQ, new WeaponSlot(new Sword(weaponConfigs[3]), m_caster, m_runner) }
        };
    }

    public void Attack(AttackSlot slot)
    {
        if(m_slots.TryGetValue(slot, out var weapon))
        {
            weapon.Attack(slot);
        }
    }
}