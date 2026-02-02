using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackSystem : MonoBehaviour
{
    [SerializeField] private Transform m_attackPoint;
    [SerializeField] private WeaponConfig[] weaponConfigs;
    
    private CoroutineRunner m_coroutines;

    // TODO inventory
    private Dictionary<AttackSlot, WeaponSlot> m_slots;
    private AttackCaster m_caster;

    private bool m_isIntialize;

    public void Initialize(CoroutineRunner coroutine)
    {
        if(m_isIntialize)
        {
            return;
        }

        m_coroutines = coroutine;

        m_caster = new AttackCaster(m_attackPoint);

        //TODO
        m_slots = new Dictionary<AttackSlot, WeaponSlot>
        {
            {AttackSlot.Main, new WeaponSlot(weaponConfigs[0], m_caster, m_coroutines)},
            {AttackSlot.Additional, new WeaponSlot(weaponConfigs[0], m_caster, m_coroutines)},
            {AttackSlot.AbilityE, new WeaponSlot(weaponConfigs[0], m_caster, m_coroutines)},
            {AttackSlot.AbilityQ, new WeaponSlot(weaponConfigs[0], m_caster, m_coroutines)},
        };


        m_isIntialize = true;   
    }

    public void Attack(AttackSlot slot)
    {
        if(m_slots.TryGetValue(slot, out var weapon))
        {
            weapon.Attack(slot);
        }
    }
}