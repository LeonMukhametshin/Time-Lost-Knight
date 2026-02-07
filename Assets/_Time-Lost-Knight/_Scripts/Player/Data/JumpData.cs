using UnityEngine;

[CreateAssetMenu(fileName = "JumpData", menuName = "Scriptable Objects/Player/JumpData")]
public class JumpData : ScriptableObject
{
    [field: SerializeField] public AnimationCurve jumpCurve { get; private set; }
    [field: SerializeField][Min(0)] public float jumpHeight { get; private set; }
    [field: SerializeField][Min(0)] public float jumpDuration { get; private set; }

    [field: SerializeField] public int maxJumps { get; private set; }
    [field: SerializeField][Min(0f)] public float cooldown { get; private set; }
}