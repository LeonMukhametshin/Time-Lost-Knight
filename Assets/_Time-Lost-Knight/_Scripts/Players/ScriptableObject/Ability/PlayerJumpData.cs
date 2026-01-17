using UnityEngine;

[CreateAssetMenu(fileName = "PlayerJumpData", menuName = "Scriptable Objects/Player/PlayerJumpData")]
public class PlayerJumpData : ScriptableObject
{
    [field: SerializeField][Min(0.1f)] public float jumpForce { get; private set; }
    [field: SerializeField][Range(0f, 1f)] public float jumpInputBufferTime { get; private set; }
}
