using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class HealthComponent : CoreComponent, IHealth, IEffectable
{   
    public event Action died;
    public event Action valueChanged;

    public float maxValue { get; private set; }
    
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

            if(value >= maxValue)
            {
                return;
            }

            if (m_value == 0)
            {
                died?.Invoke();
            }
        }
    }

    private float m_value;

    private bool m_isInitialized;

    public void Initialize(float maxHealth)
    {
        if(m_isInitialized)
        {
            return;
        }

        maxValue = maxHealth;
        value = maxHealth;
        m_isInitialized = true;
    }

    public void Heal(float value)
    {
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Heal cannot be hegative");
        }

        this.value += value;
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(damage), "Heal cannot be hegative");
        }
        this.value -= damage;
    }
}