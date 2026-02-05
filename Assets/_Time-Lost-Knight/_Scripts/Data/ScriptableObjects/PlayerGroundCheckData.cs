using UnityEngine;

[CreateAssetMenu(fileName = "PlayerGroundCheckData", menuName = "Scriptable Objects/Player/PlayerGroundCheckData")]
public class PlayerGroundCheckData : ScriptableObject
{
    [field: SerializeField] public LayerMask groundLayer { get; private set; }
    [field: SerializeField][Min(0.01f)] public float groundCheckDistance { get; private set; }
}