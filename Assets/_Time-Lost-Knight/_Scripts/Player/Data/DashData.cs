using UnityEngine;

[CreateAssetMenu(fileName = "DashData", menuName = "Scriptable Objects/Player/FallData")]
public class DashData : ScriptableObject
{
    [field: SerializeField][Min(0.1f)] public float dashSpeed { get; private set; }
    [field: SerializeField][Min(0.01f)] public float duration { get; private set; }

    [field: SerializeField][Min(0f)] public float cooldown { get; private set; }
    [field: SerializeField][Min(0f)] public int maxDashes { get; private set; }
}