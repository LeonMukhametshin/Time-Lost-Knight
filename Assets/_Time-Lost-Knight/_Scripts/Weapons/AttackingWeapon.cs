using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttackingWeapon : Weapon
{
    protected AttackingWeaponData attackingWeaponData;
    private List<IDamageable> m_detectedDamageble = new();

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
    }

    public void AddToDetected(Collider2D collision)
    {
        if(collision.TryGetComponent<IDamageable>(out var damageable))
        {
            m_detectedDamageble.Add(damageable);
            Debug.Log("AddToDetected " + collision.name);
        }
    }

    public void RemoveToDetected(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamageable>(out var damageable))
        {
            m_detectedDamageble.Remove(damageable);
            Debug.Log("RemoveToDetected " + collision.name);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}