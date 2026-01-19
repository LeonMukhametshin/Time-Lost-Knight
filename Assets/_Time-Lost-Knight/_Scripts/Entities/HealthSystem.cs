using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour, IDamageable, IEffectable
{
    public event Action died;
    public event Action<int> damaged;
    public event Action valueChanged;

    public float value
    {
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

    private float m_value;

    private bool m_isInitialize = false;

    public void Initialize(int value)
    {
        if(m_isInitialize)
        {
            return;
        }
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

    public void TakeDamage(float damage)
    {
        if (damage < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(damage), "Heal cannot be hegative");
        }

        value -= damage;
    }
}