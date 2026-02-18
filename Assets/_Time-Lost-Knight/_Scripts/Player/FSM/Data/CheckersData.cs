using UnityEngine;

[CreateAssetMenu(fileName = "CheckersData", menuName = "Scriptable Objects/Player/Checkers_Data")]
public class CheckersData : ScriptableObject
{
    [field: SerializeField][Min(0)] public float groundCheckRadius { get; private set; }
    [field: SerializeField][Min(0)] public float ceilingCheckRadius { get; private set; }
    [field: SerializeField][Min(0)] public float wallCheckDistance { get; private set; }
    [field: SerializeField] public LayerMask groundLayer { get; private set; }
    [field: SerializeField] public LayerMask platformLayer { get; private set; }
}