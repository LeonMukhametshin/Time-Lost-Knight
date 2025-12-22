using System;
using UnityEngine;

public class DamageableWall : MonoBehaviour, ICanBeDamageable
{
    [field: SerializeField] public int Health { get; private set; } = 100;

    public event Action<int> Damaged;
    public event Action Kill;

    public void TakeDamage(int damage)
    {
        Health -= damage;
        Damaged?.Invoke(damage);

        if (Health < 0)
        {
            Health = 0;
            Deastory();
        }
    }

    private void Deastory()
    {
        Kill?.Invoke();
        Destroy(gameObject);
    }
}