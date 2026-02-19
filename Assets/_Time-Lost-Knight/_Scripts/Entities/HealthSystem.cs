using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event Action died;
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

            if (m_value == 0)
            {
                died?.Invoke();
            }
        }
    }

    private float m_value;
    public float maxValue { get; private set;  }

    public void Initialize(float maxHealth)
    {
        m_value = maxHealth;
        maxValue = maxHealth;
    }

    public void Increase(float value)
    {
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Heal cannot be hegative");
        }
          
        this.value += value;
    }

    public void Decrease(float damage)
    {
        if (damage < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(damage), "Heal cannot be hegative");
        }
        Debug.Log("DO Value " + value);
        this.value -= damage;
        Debug.Log("Posle Value " + value);
    }

}