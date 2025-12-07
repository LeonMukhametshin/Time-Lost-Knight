using UnityEngine;

namespace Players
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
    public sealed class PlayerData : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float JumpForce { get; private set; }
        [field: SerializeField] public float CheckDistance { get; private set; }
        [field: SerializeField] public LayerMask GroundLayer { get; private set; }
        [field: SerializeField] public PhysicsMaterial2D JumpMaterial { get; private set; }
    }
}