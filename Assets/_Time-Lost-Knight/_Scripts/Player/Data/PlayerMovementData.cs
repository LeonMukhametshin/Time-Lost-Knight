using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMovementData", menuName = "Scriptable Objects/Player/PlayerMovementData")]
public sealed class PlayerMovementData : ScriptableObject
{
    [field: SerializeField] public WalkData moveData { get; private set; }
    [field: SerializeField] public JumpData jumpData { get; private set; }
    [field: SerializeField] public DashData dashData { get; private set; }
    [field: SerializeField] public FallData fallData { get; private set; }
    [field: SerializeField] public GroundCheckData groundCheckData { get; private set; }
}