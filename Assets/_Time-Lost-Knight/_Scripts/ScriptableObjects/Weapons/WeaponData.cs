using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/Weapon_Data")]
public abstract class WeaponData : ScriptableObject
{
     public int amountOfAttacks {get; protected set;}
     public float[] movementSpeed { get; protected set; }
}