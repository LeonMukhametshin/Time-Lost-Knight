using Interfaces;
using UnityEngine;

public class Health : IDamageable
{
    private float m_value;

    public float Value
    {
        get => m_value;
        protected set
        {
            m_value = value > 0 ? value : 0;
        }
    }

    public Health(int healthpoint)
    {
        Value = healthpoint;
    }

    public void TakeDamage(float damage)
    {
        float actualDamage = CalculateActualDamage(damage);
        ApplyDamage(actualDamage);
    }

    protected virtual float CalculateActualDamage(float damage)
    {
        return Mathf.Max(0, damage);
    }

    private void ApplyDamage(float actualDamage)
    {
        m_value -= actualDamage;

        if(m_value <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {

    }
}
