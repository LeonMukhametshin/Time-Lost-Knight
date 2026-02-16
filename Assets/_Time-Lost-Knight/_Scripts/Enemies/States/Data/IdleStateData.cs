using UnityEngine;

[CreateAssetMenu(fileName = "IdleState_Data", menuName = "Scriptable Objects/State Data/IdleState_Data")]
public class IdleStateData : ScriptableObject
{
    [field: SerializeField][Range(0f, 8f)] public float minIdleTime { get; private set; }
    [field: SerializeField][Range(0f, 8f)] public float maxIdleTime { get; private set; }
}