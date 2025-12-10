using UnityEngine;

[CreateAssetMenu(fileName = "JumpAbilityConfig", menuName = "Scriptable Objects/JumpAbilityConfig")]
public sealed class JumpAbilityConfig : AbilityConfig
{
    [field: SerializeField] public float JumpForceMultiplier { get; private set; } = 1f;
    [field: SerializeField] public int MaxJumpsInAir { get; private set;  } = 1;
    [field: SerializeField] public float JumpCooldown { get; private set; } = 0.2f;
    [field: SerializeField] public bool CanJumpWhileMoving { get; private set; } = true;
    [field: SerializeField] public bool RequiresGroundedForJump { get; private set; } = true;
    [field: SerializeField] public float CoyoteTime { get; private set; } = 0.1f;
}