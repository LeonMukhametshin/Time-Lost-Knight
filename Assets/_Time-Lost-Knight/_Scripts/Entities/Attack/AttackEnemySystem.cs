using System;
using System.Collections;
using UnityEngine;

public class AttackEnemySystem : MonoBehaviour
{
    [SerializeField] private CoroutineRunner m_coroutineRunner;
    private Coroutine m_attackRoutine;

    public event Action<AttackState> StateCnanged;

    private Transform m_target;
    private Transform m_transform;

    private WeaponConfig m_weaponConfig;
    private AttackCaster m_caster;

    private AttackState m_state;

    public AttackState state
    {
        get => m_state;
        set
        {
            if (m_state != value)
            {
                m_state = value;
                StateCnanged?.Invoke(state);
            }
        }
    }

    private bool m_isInitialized;

    public void Initialize(WeaponConfig config, Transform target)
    {
        m_weaponConfig = config;
        m_target = target;
        m_transform = transform;

        m_caster = new AttackCaster(m_transform);

        m_isInitialized = true;
    }

    //TODO remove
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!m_isInitialized || !m_target)
        {
            return;
        }

        if (collision.gameObject.TryGetComponent<IEffectable>(out var effectable))
        {
            Debug.Log(collision.name);
            TryAttack();
        }
    }

    public bool TryAttack()
    {
        if(m_coroutineRunner is not null)
        {
            m_coroutineRunner.Stop(m_attackRoutine);
        }

        m_attackRoutine = m_coroutineRunner.Run(AttackRoutine());

        return true;
    }

    private IEnumerator AttackRoutine()
    {
        state = AttackState.Windup;
        yield return new WaitForSeconds(m_weaponConfig.windupTime);

        state = AttackState.Attacking;
        m_caster.Cast(m_weaponConfig);

        state = AttackState.Cooldown;
        yield return new WaitForSeconds(0.3f);

        state = AttackState.Idle;
        m_coroutineRunner = null;
    }
}