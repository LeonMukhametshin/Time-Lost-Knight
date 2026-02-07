using UnityEngine;

[CreateAssetMenu(fileName = "GroundCheckData", menuName = "Scriptable Objects/Player/GroundCheckData")]
public class GroundCheckData : ScriptableObject
{
    [field: SerializeField] public LayerMask groundLayer { get; private set; }
    [field: SerializeField][Min(0.01f)] public float groundCheckDistance { get; private set; }
}