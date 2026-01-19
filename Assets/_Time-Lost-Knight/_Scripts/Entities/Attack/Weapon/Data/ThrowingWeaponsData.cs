using UnityEngine;

[CreateAssetMenu(fileName = "ThrowingWeaponsData", menuName = "Scriptable Objects/ThrowingWeaponsData")]
public class ThrowingWeaponsData : WeaponConfig
{
    [field: SerializeField] public AnimationCurve directionCurve { get; private set; }
}   