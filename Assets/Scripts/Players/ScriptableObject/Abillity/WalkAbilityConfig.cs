using UnityEngine;

[CreateAssetMenu(fileName = "WalkAbilityConfig", menuName = "Scriptable Objects/WalkAbilityConfig")]
public sealed class WalkAbilityConfig : AbilityConfig
{
    [field: SerializeField] public float WalkSpeed { get; private set; } = 1f;
    [field: SerializeField] public float AccelerationMultiplier { get; private set; } = 1f;
    [field: SerializeField] public bool CanWalkInAir { get; private set; } = false;
}