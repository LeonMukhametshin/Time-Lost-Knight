using System;
using UnityEngine;

[Serializable]
public struct WeaponAttackDetails
{
    public string attackName;
    public float movementSpeed;

    [SerializeReferenceDropdown]
    [SerializeReference] public IEffect[] effects;
}