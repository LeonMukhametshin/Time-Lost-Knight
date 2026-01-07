using UnityEngine;

public class MeleeHitbox : MonoBehaviour, IAttack
{
    private AttackData m_data;

    private float m_timer = 0f;
    private bool m_isInitialized = false;

    public void Initialize(AttackData data)
    {
        if(m_isInitialized)
        {
            return;
        }

        m_data = data;
    }

    private void Update()
    {
        if(m_timer > 0)
        {
            m_timer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is IDamageable damageable)
        {
            TryAttack(damageable);
        }
    }

    public bool TryAttack(IDamageable damageable)
    {
        if(m_timer <= 0)
        {
            Attack(damageable);
            m_timer = m_data.attackCooldown;
            return true;
        }

        return false;
    }

    private void Attack(IDamageable damageable)
    {
        damageable.TakeDamage(m_data.damage);
    }
}