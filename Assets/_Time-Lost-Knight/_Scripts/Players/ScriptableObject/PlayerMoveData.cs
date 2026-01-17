using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMoveData", menuName = "Scriptable Objects/Player/PlayerMoveData")]
public class PlayerMoveData : ScriptableObject
{
    [field: SerializeField][Min(0.1f)] public float runMaxSpeed { get; private set; }
    [field: SerializeField][Min(0.1f)] public float runAcceleration { get; private set; }
    [field: SerializeField][Min(0.1f)] public float runDeceleration { get; private set; }
    [field: SerializeField][Range(0f, 1f)] public float airAccelMultiplier { get; private set; }
}