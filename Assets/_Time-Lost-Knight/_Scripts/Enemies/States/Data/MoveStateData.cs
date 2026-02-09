using UnityEngine;

[CreateAssetMenu(fileName = "MoveState_Data", menuName = "Scriptable Objects/State Data/MoveState_Data")]
public class MoveStateData : ScriptableObject
{
    [field: SerializeField][Min(0f)] public float movementSpeed { get; private set; }
}