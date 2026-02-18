using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class AttackingWeapon : Weapon
{
    protected FlipContoller flipContoller
    {
        get => m_flipContoller ??= core.GetCoreComponent<FlipContoller>();
    }
    private FlipContoller m_flipContoller;

    protected AttackingWeaponData attackingWeaponData;
    private List<IDamageable> m_detectedDamageble = new();
    private List<IKnockbackable> m_detectedKnockbackables = new();

    protected override void Awake()
    {
        base.Awake();

        if(weaponData.GetType() == typeof(AttackingWeaponData))
        {
            attackingWeaponData = (AttackingWeaponData)weaponData;
        }
    }

    public override void AnimationActionTriger()
    {
        base.AnimationActionTriger();

        CheckMeleeAttack();
    }

    private void CheckMeleeAttack()
    {
        var details = attackingWeaponData.attackDetails[attackCounter];

        foreach (var item in m_detectedDamageble.ToList())
        {
            item.TakeDamage(details.damageAmount);
        }

        foreach(var item in m_detectedKnockbackables.ToList())
        {
            item.Knockback(details.angle, details.knokbackStringht, flipContoller.facingDirection);
        }
    }

    public void AddToDetected(Collider2D collision)
    {
        if(collision.TryGetComponent<IDamageable>(out var damageable))
        {
            m_detectedDamageble.Add(damageable);
        }

        if(collision.TryGetComponent<IKnockbackable>(out var knockbackable))
        {
            m_detectedKnockbackables.Add(knockbackable);
        }
    }

    public void RemoveToDetected(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamageable>(out var damageable))
        {
            m_detectedDamageble.Remove(damageable);
        }

        if (collision.TryGetComponent<IKnockbackable>(out var knockbackable))
        {
            m_detectedKnockbackables.Remove(knockbackable);
        }
    }
}