using UnityEngine;

[CreateAssetMenu(fileName = "DistanceWeaponData", menuName = "Scriptable Objects/DistanceWeaponData")]
public class DistanceWeaponData : WeaponConfig
{
    [field: SerializeField][Min(0)] public float speed { get; private set; }
}