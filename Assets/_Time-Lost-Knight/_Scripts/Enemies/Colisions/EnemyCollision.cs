using System;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    public event Action touchEnemy;

    [SerializeField] private float m_knockbackForce;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<IDamageable>(out var damageable))
        {
            touchEnemy?.Invoke();
        }
    }
}