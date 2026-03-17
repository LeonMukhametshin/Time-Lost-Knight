using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.ScriptableObjects.Weapons
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/Weapon_Data")]
    [MovedFrom("")]
    public abstract class WeaponData : ScriptableObject
    {
         public int amountOfAttacks {get; protected set;}
         public float[] movementSpeed { get; protected set; }
    }
}
