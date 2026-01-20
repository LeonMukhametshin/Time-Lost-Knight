using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    [SerializeField] private Transform m_attackPoint;

    [SerializeField] private WeaponConfig[] weaponConfigs;
    [SerializeField] private CoroutineRunner m_runner;

    // TODO inventory
    private Dictionary<AttackSlot, WeaponSlot> m_slots;
    private AttackCaster m_caster;

    private void Awake()
    {
        m_caster = new AttackCaster(m_attackPoint);

        //TODO
        m_slots = new Dictionary<AttackSlot, WeaponSlot>
        {
            {AttackSlot.Main, new WeaponSlot(weaponConfigs[0], m_caster, m_runner)},
            {AttackSlot.Additional, new WeaponSlot(weaponConfigs[1], m_caster, m_runner)},
            {AttackSlot.AbilityE, new WeaponSlot(weaponConfigs[0], m_caster, m_runner)},
            {AttackSlot.AbilityQ, new WeaponSlot(weaponConfigs[0], m_caster, m_runner)},
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