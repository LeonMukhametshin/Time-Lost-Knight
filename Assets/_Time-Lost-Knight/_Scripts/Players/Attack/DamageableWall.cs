using System;
using UnityEngine;

public class DamageableWall : MonoBehaviour, IDamageable, IEffectable
{
    [field: SerializeField] public float Health { get; private set; } = 100;

    public event Action<float> Damaged;
    public event Action Kill;

    public void TakeDamage(float damage)
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