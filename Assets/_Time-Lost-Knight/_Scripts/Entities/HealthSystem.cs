using System;
using UnityEngine;

public class HealthSystem : IDamageable
{
    public event Action died;
    public event Action<int> damaged;
    public event Action valueChanged;

    private int m_value;

    public int value {
        get => m_value;
        private set
        {
            if (Mathf.Approximately(m_value, value))
            {
                return;
            }
            m_value = value < 0 ? 0 : value;

            valueChanged?.Invoke();

            if (m_value is 0)
            {
                died?.Invoke();
            }

            Debug.Log("Player HP: " + m_value);
        }
    }

    public HealthSystem(int value)
    {
        m_value = value;
    }

    public void Heal(int heal)
    {
        if (heal < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(heal), "Heal cannot be hegative");
        }
          
        value += heal;
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(damage), "Heal cannot be hegative");
        }

        value -= damage;
    }
}