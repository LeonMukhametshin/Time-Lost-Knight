using Attacks;
using System;

public class CalculateCritDamage : IDamageCalculator
{
    public int Calculate(WeaponConfig config)
    {
        if (UnityEngine.Random.Range(0, 100) < config.CriticalChance)
        {
            return (int)Math.Ceiling(config.CriticalMultiplier * config.Damage);
        }
        else
        {
            return config.Damage;
        }
    }
}