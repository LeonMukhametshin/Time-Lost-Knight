using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDashData", menuName = "Scriptable Objects/Player/PlayerDashData")]
public class PlayerDashData : ScriptableObject
{
    [field: SerializeField][Min(0.1f)] public float dashSpeed { get; private set; }
    [field: SerializeField][Min(0.01f)] public float duration { get; private set; }
}