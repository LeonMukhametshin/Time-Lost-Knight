using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDashData", menuName = "Scriptable Objects/Player/PlayerDashData")]
public class PlayerDashData : ScriptableObject
{
    [field: SerializeField][Range(0, 5)] public int maxDashes { get; private set; }
    [field: SerializeField][Min(0.1f)] public float dashSpeed { get; private set; }
    [field: SerializeField][Min(0.01f)] public float dashAttackTime { get; private set; }
    [field: SerializeField][Range(0f, 1f)] public float dashInputBufferTime { get; private set; }
}
