using System;
using UnityEngine;

public class Stats : CoreComponent
{
    public event Action died;

    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    public override void Awake()
    {
        base.Awake();

        currentHealth = maxHealth;
    }

    public void DecreaseHealth(float value)
    {
        if(value <= 0 )
        {
            throw new ArgumentOutOfRangeException(nameof(value), "value cannot be hegative");
        }

        currentHealth -= value;

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            died?.Invoke();
        }
    }

    public void IncreaseHealth(float value)
    {
        if (value <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "value cannot be hegative");
        }

        currentHealth = Mathf.Clamp(currentHealth + value, 0, maxHealth);
    }
}