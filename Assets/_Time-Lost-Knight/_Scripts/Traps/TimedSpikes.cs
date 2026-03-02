using UnityEngine;

public class TimedSpikes : Trap
{
    [SerializeField][Range(0, 10)] private float duration;
    [SerializeField] private TrapAttackDetails m_attackDetails;
    private float m_timer;

    private void Update()
    {
        if (Pause.instants.isPaused)
        {
            return;
        }

        if(Time.time >= m_timer + duration)
        {
            Activate();

            m_timer = Time.time;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damage(collision);
    }

    public override void Damage(Collider2D collision)
    {
        base.Damage(collision);

        if (collision.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.TakeDamage(m_attackDetails.damageAmount);
        }

        if (collision.TryGetComponent<IKnockbackable>(out var knockbackable))
        {
            knockbackable.Knockback(m_attackDetails.angle, m_attackDetails.knokbackStringht);
        }
        Debug.Log($"{collision.name} damage from {this.name} in amount of {damage}");
    }

    public override void Activate()
    {
        base.Activate();
    }
}