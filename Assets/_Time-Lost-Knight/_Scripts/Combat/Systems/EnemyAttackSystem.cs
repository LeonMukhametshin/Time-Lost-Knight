using UnityEngine;

public class EnemyAttackSystem : MonoBehaviour
{
    private WeaponConfig m_weaponConfig;
    private AttackCaster m_caster;

    private float m_attackTime;
    private float m_cooldownTimer;

    private bool m_isInitialized;

    public void Initialize(WeaponConfig config, float attackTime)
    {
        if(m_isInitialized)
        {
            return;
        }

        m_weaponConfig = config;
        m_attackTime = attackTime;
        m_caster = new AttackCaster(transform);

        m_isInitialized = true;
    }

    private void Update()
    {
        if (!m_isInitialized)
        {
            return;
        }

        if(m_cooldownTimer > 0)
        {
            m_cooldownTimer -= Time.deltaTime;
        }
    }

    public bool TryAttack()
    {
        if(!m_isInitialized)
        {
            return false;
        }

        if(m_cooldownTimer > 0)
        {
            return false;
        }

        m_caster.Cast(m_weaponConfig);
        m_cooldownTimer = m_attackTime;

        return true;
    }
}