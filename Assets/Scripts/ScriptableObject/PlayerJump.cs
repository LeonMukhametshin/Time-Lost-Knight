using UnityEngine;

namespace Players
{
    [CreateAssetMenu(fileName = "PlayerJump", menuName = "Scriptable Objects/PlayerJump")]
    public sealed class PlayerData : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float JumpForce { get; private set; }
        [field: SerializeField]  public float CheckDistance { get; private set; }
        [field: SerializeField]  public LayerMask GroundLayer { get; private set; }
    }
}