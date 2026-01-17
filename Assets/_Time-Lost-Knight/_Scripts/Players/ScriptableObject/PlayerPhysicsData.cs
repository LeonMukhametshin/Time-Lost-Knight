using UnityEngine;

[CreateAssetMenu(fileName = "PlayerPhysicsData", menuName = "Scriptable Objects/Player/PlayerPhysicsData")]
public class PlayerPhysicsData : ScriptableObject
{
    [Header("Gravity")]
    [field: SerializeField][Min(0.1f)] public float gravityScale { get; private set; }
    [field: SerializeField][Min(1f)] public float fallGravityMultiplier { get; private set; }
    [field: SerializeField] public float maxFallSpeed { get; private set; }

    [Header("Physics Materials")]
    [field: SerializeField] public PhysicsMaterial2D baseMaterial { get; private set; }
    [field: SerializeField] public PhysicsMaterial2D jumpMaterial { get; private set; }
}
