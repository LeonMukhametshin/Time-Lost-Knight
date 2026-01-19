using Attacks;
using UnityEngine;

public sealed class AttackCaster
{
    public void Cast(IWeapon weapon)
    {
        Debug.Log(weapon.config.ToString());
    }
}