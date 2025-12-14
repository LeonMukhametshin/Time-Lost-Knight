using UnityEngine;

public class DamageableWall : MonoBehaviour, ICanBeDamageable
{
    [field: SerializeField] public int Health { get; private set; } = 100;

    public void TakeDamage(DamageType type, int damage)
    {
        Health -= damage;
        Debug.Log($"TakeDamage with damage: {damage}, damage type: {type}, current health: {Health}");

        if(Health < 0)
        {
            Health = 0;
            Deastory();
        }
    }

    private void Deastory()
    {
        Destroy(gameObject);
    }
}