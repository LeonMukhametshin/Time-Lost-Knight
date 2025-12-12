using Attacks;
using System;

public class CalculateCritDamage : IDamageCalculator
{
    public int Calculate(WeaponConfig config)
    {
        if(config is CritWeaponConfig conf)
        {
            if (UnityEngine.Random.Range(0, 100) < conf.CriticalChance)
            {
                return (int)Math.Ceiling(conf.CriticalMultiplier * config.Damage);
            }
        }
        
        return config.Damage;
    }
}