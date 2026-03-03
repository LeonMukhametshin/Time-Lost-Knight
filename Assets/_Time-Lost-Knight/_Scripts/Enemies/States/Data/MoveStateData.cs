using UnityEngine;

[CreateAssetMenu(fileName = "MoveState", menuName = "Scriptable Objects/Enemy/States/MoveState")]
public sealed class MoveStateData : ScriptableObject
{
    [field: SerializeField][Min(0f)] public float movementSpeed { get; private set; }
}