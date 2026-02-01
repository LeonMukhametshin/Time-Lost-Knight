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
            if(collision.TryGetComponent<IPhysics>(out var physics))
            {
                var direction = (collision.gameObject.transform.position - transform.position).normalized;

                physics.AddForce(direction * m_knockbackForce, ForceMode2D.Impulse);
            }

            touchEnemy?.Invoke();
        }
    }
}