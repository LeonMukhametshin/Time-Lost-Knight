using UnityEngine;

public class StaticTrap : Trap
{
    [SerializeField] private TrapAttackDetails m_trapAttackDetails;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Damage(collision);
    }

    public override void Damage(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.TakeDamage(m_trapAttackDetails.damageAmount);
        }

        if(collision.TryGetComponent<IKnockbackable>(out var knockbackable))
        {
            //knockbackable.Knockback(m_trapAttackDetails.angle, m_trapAttackDetails.knokbackStringht);
        }
    }
}