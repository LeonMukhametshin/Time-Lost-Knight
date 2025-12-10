using UnityEngine;

[CreateAssetMenu(fileName = "CharacterMovementConfig", menuName = "Scriptable Objects/CharacterMovementConfig")]
public class CharacterMovementConfig : ScriptableObject
{
    [field: SerializeField] public float MaxHorizontalSpeed { get; set; } = 5f;
    [field: SerializeField] public float Acceleration { get; set; } = 20f;
    [field: SerializeField] public float Deceleration { get; set; } = 15f;
    [field: SerializeField] public float JumpForce { get; set; } = 12f;
    [field: SerializeField] public float GravityScale { get; set; } = 9.8f;
    [field: SerializeField] public float GroundDrag { get; set; } = 0.1f;
    [field: SerializeField] public float AirDrag { get; set; } = 0.05f;
    [field: SerializeField] public float MaxFallSpeed { get; set; } = -20f;
    [field: SerializeField] public float GroundCheckDistance { get; set; } = 0.1f;
    [field: SerializeField] public LayerMask GroundLayer { get; set; }
}