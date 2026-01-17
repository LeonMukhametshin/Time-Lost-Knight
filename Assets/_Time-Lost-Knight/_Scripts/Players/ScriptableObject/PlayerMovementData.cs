using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMovementData", menuName = "Scriptable Objects/Player/PlayerMovementData")]
public sealed class PlayerMovementData : ScriptableObject
{
    [field: SerializeField] public PlayerMoveData m_moveData { get; private set; }
    [field: SerializeField] public PlayerJumpData m_jumpData { get; private set; }
    [field: SerializeField] public PlayerDashData m_dashData { get; private set; }
    [field: SerializeField] public PlayerGroundCheckData m_groundCheckData { get; private set; }
}