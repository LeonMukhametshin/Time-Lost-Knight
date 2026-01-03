using System;

public class HealthSystem : IDamageable
{
    public int value { get; private set; }

    public event Action Death;
    public event Action<int> Damaged;

    public HealthSystem(int maxHealth)
    {
        value = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if(amount <= 0 || value <= 0)
        {
            return;
        }

        value -= amount;
        Damaged?.Invoke(amount);

        if(value <= 0 )
        {
            value = 0;
            Death?.Invoke();
        }
    }
}