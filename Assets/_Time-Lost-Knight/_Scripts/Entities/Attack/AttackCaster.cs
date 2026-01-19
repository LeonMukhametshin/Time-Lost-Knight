using System;
using UnityEngine;

public sealed class AttackCaster
{
    private readonly Transform m_attackPoint;

    public AttackCaster(Transform casterTransform)
    {
        m_attackPoint = casterTransform;
    }

    public void Cast(WeaponConfig weapon, Vector3 worldPosition)
    {
        if(!weapon)
        {
            return;
        }

        switch(weapon)
        {
            case MeleeWeaponData melee: CastMelee(melee); break;
            case DistanceWeaponData distanceData: CastDistance(distanceData); break;
            case ThrowingWeaponsData throwingWeaponsData: CastThrowing(throwingWeaponsData); break; 
        }
    }

    private void CastMelee(MeleeWeaponData weapon)
    {
        if(!weapon.view)
        {
            throw new NullReferenceException("Target attack must have view");
        }

        var projectile = UnityEngine.Object.Instantiate(weapon.view, m_attackPoint);
        SetLayer(projectile);

        var meleeProjectile = 
            projectile.GetComponent<ISpellMelee>() ?? 
            projectile.AddComponent<SpellMelee>();

        meleeProjectile.Initialize(m_attackPoint.position, weapon.attackZone, weapon.effects);
    }

    private void CastDistance(DistanceWeaponData distanceData)
    {
        if(!distanceData)
        {
            return;
        }

        var projectile = UnityEngine.Object.Instantiate(distanceData.view, m_attackPoint);
        SetLayer(projectile);

        var distanceProjectile = 
            projectile.GetComponent<ISpellProjectile>() ??
            projectile.AddComponent<SpellProjectile>();

        distanceProjectile.Initialize(distanceData.speed, distanceData.directionCurve, distanceData.effects);
        projectile.transform.SetParent(null, true);
    }

    private void CastThrowing(ThrowingWeaponsData throwingWeaponsData)
    {
        if(!throwingWeaponsData)
        {
            return;
        }

        var projectile = UnityEngine.Object.Instantiate(throwingWeaponsData.view, m_attackPoint);
        SetLayer(projectile);

        var throwingProjectile =
            projectile.GetComponent<ISpellThrowing>() ??
            projectile.AddComponent<SpellThrowing>();

        throwingProjectile.Initialize(m_attackPoint.position, 
            throwingWeaponsData.directionCurve, 
            throwingWeaponsData.effects);
    }

    private void SetLayer(GameObject visualEffect) =>
        visualEffect.layer = m_attackPoint.gameObject.layer;
}