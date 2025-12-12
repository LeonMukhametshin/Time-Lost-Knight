using UnityEngine;

public class DamageableWall : ICanBeDamageable
{
    public int Health { get; private set; }

    public DamageableWall(int health)
    { 
        Health = health; 
    }

    public void TakeDamage(DamageType type, int damage)
    {
        Health -= damage;
        Debug.Log($"TakeDamage with damage: {damage}, damage type: {type}");
    }
}
