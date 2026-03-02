using UnityEngine;
using System.Collections.Generic;

public class AttackingWeapon : Weapon
{
    protected AttackingWeaponData attackingWeaponData;

    private List<IEffectable> m_effectables = new();

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
        if(m_effectables is null)
        {
            return;
        }

        var details = attackingWeaponData.attackDetails[attackCounter];

        foreach (var effectable in m_effectables)
        {
            details.effects.ApplyEffect(effectable);
        }
    }

    public void AddToDetected(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Core core))
        {       
            m_effectables.AddRange(core.effectables);           
        }
    }

    public void ClearDetectedList(Collider2D collision) => 
        m_effectables.Clear();
}