using UnityEngine;

[CreateAssetMenu(fileName = "MoveData", menuName = "Scriptable Objects/Player/MoveData")]
public class WalkData : ScriptableObject
{
    [field: SerializeField][Min(0.1f)] public float runMaxSpeed { get; private set; }
    [field: SerializeField][Min(0.1f)] public float runAcceleration { get; private set; }
    [field: SerializeField][Min(0.1f)] public float runDeceleration { get; private set; }
}