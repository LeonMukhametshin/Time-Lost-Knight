using Game.Buffs.Interfaces;
using System;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.ScriptableObjects.Weapons
{
    [Serializable]
    [MovedFrom("")]
    public struct WeaponAttackDetails
    {
        public string attackName;
        public float movementSpeed;

        [SerializeReferenceDropdown]
        [SerializeReference] public IEffect[] effects;
    }
}
