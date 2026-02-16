using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackingWeaponData", menuName = "Scriptable Objects/Attacking_Weapon_Data")]
public class AttackingWeaponData : WeaponData
{
    [field: SerializeField] private WeaponAttackDetails[] m_attackDetails;

    public IReadOnlyList<WeaponAttackDetails> attackDetails
    { 
        get => m_attackDetails;
        protected set => attackDetails = value;
    }
 

    private void OnEnable()
    {
        amountOfAttacks = m_attackDetails.Length;
        movementSpeed = new float[amountOfAttacks];

        for (int i = 0; i < amountOfAttacks; i++)
        {
            movementSpeed[i] = m_attackDetails[i].movementSpeed;
        }
    }
}