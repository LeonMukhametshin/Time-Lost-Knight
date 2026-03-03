using UnityEngine;

public class ExplosiveProjectile : BaseProjectile
{
    [Header("Explosion")]
    [SerializeField] private float m_explosionRadius = 2f;
    [SerializeField] private GameObject m_explosionVfx;

    protected override void OnHitEnemy(Collider2D other)
    {
        Explode();
    }

    protected override void OnHitGround(Collider2D other)
    {
        Explode();
    }

    private void Explode()
    {
        if (m_isFinished)
            return;

        StopProjectile(); // останавливаем движение

        if (m_explosionVfx != null)
            Instantiate(m_explosionVfx, transform.position, Quaternion.identity);

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, m_explosionRadius, m_enemyLayer);
        foreach (Collider2D hit in hits)
        {
            if (TryGetDamageable(hit, out var damageable))
                damageable.TakeDamage(m_data.damage);
        }

        Finish(); // уничтожаем снаряд
    }

    // Для отладки радиуса взрыва в редакторе
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, m_explosionRadius);
    }
}