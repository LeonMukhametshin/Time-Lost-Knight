using System;

public class HealthSystem : IDamageable
{
    public int MaxHealth { get; }
    public int CurrentHealth { get; private set; }

    public event Action Death;
    public event Action<int> Damaged;

    public HealthSystem(int maxHealth, int currentHealth)
    {
        MaxHealth = maxHealth;
        CurrentHealth = currentHealth;
    }

    public void TakeDamage(int amount)
    {
        if(amount <= 0 || CurrentHealth <= 0)
        {
            return;
        }

        CurrentHealth -= amount;
        Damaged?.Invoke(amount);

        if(CurrentHealth <= 0 )
        {
            CurrentHealth = 0;
            Death?.Invoke();
        }
    }
}
