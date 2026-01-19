using System;
using System.Collections;
using UnityEngine;

public sealed class WeaponSlot
{
    public event Action<AttackState> StateCnanged;
    public event Action AttackCanceled;

    public WeaponConfig m_weapon;

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

    private AttackCaster m_caster;

    private readonly CoroutineRunner m_runner;
    private Coroutine m_attackRoutine;

    private Transform m_transform;

    public WeaponSlot(WeaponConfig weapon, AttackCaster caster, 
        CoroutineRunner runner, Transform transform)
    {
        m_weapon = weapon;
        m_caster = caster;
        m_runner = runner;
        m_transform = transform;
    }

    public void SetWeapon(WeaponConfig weapon)
    {
        m_weapon = weapon;
    }

    public void Attack(AttackSlot slot)
    {
        if (state is not AttackState.Idle)
        {
            return;
        }

        TryAttackRoutine();
    }

    public void CancelAttack()
    {
        if (state is AttackState.Windup)
        {
            AttackCanceled?.Invoke();
        }
    }

    private void TryAttackRoutine()
    {
        if(m_attackRoutine is not null)
        {
            m_runner.Stop(m_attackRoutine);
        }

        m_attackRoutine = m_runner.Run(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        state = AttackState.Windup;
        yield return new WaitForSeconds(0.2f);

        state = AttackState.Attacking;
        m_caster.Cast(m_weapon, m_transform.position);

        state = AttackState.Cooldown;
        yield return new WaitForSeconds(0.3f);

        state = AttackState.Idle;
        m_attackRoutine = null;
    }
}