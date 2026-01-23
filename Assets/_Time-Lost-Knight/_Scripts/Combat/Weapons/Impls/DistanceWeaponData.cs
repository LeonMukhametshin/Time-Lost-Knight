using UnityEngine;

[CreateAssetMenu(fileName = "DistanceWeaponData", menuName = "Scriptable Objects/Weapon/DistanceWeaponData")]
public class DistanceWeaponData : WeaponConfig
{
    [field: SerializeField] public AnimationCurve directionCurve { get; private set; }
    [field: SerializeField][Min(0)] public int amount { get; private set; } 
    [field: SerializeField][Min(0)] public float speed { get; private set; }
}