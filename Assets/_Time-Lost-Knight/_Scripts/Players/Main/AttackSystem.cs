using Attacks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    [SerializeField] private MeleeWeaponConfig m_weaponConfig;

    public event Action<AttackState> StateCnanged;
    public event Action AttackCanceled;

    private Dictionary<AttackSlot, IWeapon> m_weapons;

    public IWeapon m_weapon;

    public AttackState state
    {
        get => m_state;
        set
        {
            if (m_state != value)
            {
                m_state = value;
                StateCnanged?.Invoke(m_state);
            }
        }
    }

    private AttackState m_state = AttackState.Idle;

    private Coroutine m_cooldownCoroutine;

    //TODO remove
    private void Start()
    {
        m_weapons = new Dictionary<AttackSlot, IWeapon>()
        {
            {AttackSlot.Main, new Sword(m_weaponConfig)},
            {AttackSlot.Additional, null },
            {AttackSlot.AbilityE, null },
            {AttackSlot.AbilityQ, null },
        };
    }

    public void SetWeapon(AttackSlot slot, IWeapon weapon)
    {
        m_weapons[slot] = weapon;
    }

    public void Attack(AttackSlot slot)
    {
        if (state is not AttackState.Idle)
        {
            return;
        }

        if (!m_weapons.TryGetValue(slot, out var weapon) || weapon == null)
        {
            return;
        }

        m_weapon = weapon;
        TryAttackRoutine();
    }

    public void CancelAttack()
    { 
        if(state is AttackState.Windup)
        {
            AttackCanceled?.Invoke();
        }
    }

    private void TryAttackRoutine()
    {
        if(m_cooldownCoroutine is not null)
        {
            StopCoroutine(m_cooldownCoroutine);
        }

        m_cooldownCoroutine = StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        state = AttackState.Windup;
        yield return new WaitForSeconds(m_weapon.config.windupTime);

        state = AttackState.Attacking;
        //m_weapon.ApplyDamage();
        Debug.Log("Attack!");

        state = AttackState.Cooldown;
        yield return new WaitForSeconds(m_weapon.config.cooldown);

        state = AttackState.Idle;
        m_cooldownCoroutine = null;
    }
}