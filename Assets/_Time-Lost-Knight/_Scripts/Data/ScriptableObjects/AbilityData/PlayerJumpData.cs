using UnityEngine;

[CreateAssetMenu(fileName = "PlayerJumpData", menuName = "Scriptable Objects/Player/PlayerJumpData")]
public class PlayerJumpData : ScriptableObject
{
    [field: SerializeField] public AnimationCurve jumpCurve { get; private set; }
    [field: SerializeField][Min(0)] public float jumpHeight { get; private set; }
    [field: SerializeField][Min(0)] public float jumpDuration { get; private set; }
}