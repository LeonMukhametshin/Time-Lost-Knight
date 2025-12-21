using UnityEngine;

[CreateAssetMenu(fileName = "GroundCheckData", menuName = "Scriptable Objects/Ground Check Data")]
public class GroundCheckData : ScriptableObject
{
    [Header("Ground Check Settings")]
    [field: SerializeField] public float groundCheckDistance { get; private set; }
    [field: SerializeField] public float obstacleCheckDistance { get; private set; }
    [field: SerializeField] public LayerMask groundLayer { get; private set; }
    [field: SerializeField] public LayerMask obstacleLayer { get; private set; }
}
