using System;
using UnityEngine;

public class HealthComponent : CoreComponent, IHealth
{   
    public event Action died;
    public event Action valueChanged;

    [field: SerializeField] public float maxValue { get; private set; }
    
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

    public override void Awake()
    {
        base.Awake();

        m_value = maxValue;
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