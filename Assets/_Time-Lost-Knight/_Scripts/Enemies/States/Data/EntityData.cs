using UnityEngine;

[CreateAssetMenu(fileName = "Entity_Data", menuName = "Scriptable Objects/Entity_Data")]
public class EntityData : ScriptableObject
{
    [field: SerializeField][Min(0)] public float wallCheckDistance { get; private set; }
    [field: SerializeField][Min(0)] public float groundCheckDistance { get; private set; }
    [field: SerializeField] public LayerMask groundLayer { get; private set; }
}