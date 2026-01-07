using UnityEngine;

public class MeleeAttack 
{
    private IWeapon m_weapon;

    private Transform m_attackPoint;
    private Vector2 m_attackArea;

    public MeleeAttack(IWeapon weapon, Transform attackPoint, Vector2 attackArea)
    {
        m_weapon = weapon;
        m_attackPoint = attackPoint;
        m_attackArea = attackArea;
    }

    public void SetWeapon(IWeapon weapon)
    {
        m_weapon = weapon;
    }

    public void PerformAttack()
    {
        var hits = Physics2D.OverlapBoxAll(
            m_attackPoint.position,
            m_attackArea,
            0f);

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out ICanBeDamageable damageable))
            {
                m_weapon.ApplyDamage(damageable);
            }
        }
    }
}