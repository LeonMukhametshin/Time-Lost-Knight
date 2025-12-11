using UnityEngine;

public class Sword : WeaponDecorator
{
    private float criticalChance;
    private float criticalMultiplier;

    public float CriticalMultiplier
    {
        get => criticalMultiplier; 
        set => criticalMultiplier = value;
    }

    public float CriticalChance
    {
        get => criticalChance;
        private set => criticalChance = Mathf.Clamp(value, 0f, 100f);
    }


    public Sword(IWeapon weapon, DamageType type, float criticalChance) : base(weapon, type)
    {
    }
}