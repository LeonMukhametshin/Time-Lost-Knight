using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public sealed class PlayerData : ScriptableObject
{
    [Header("Health")]
    [field: SerializeField][Min(0)] public int healthPoints { get; private set; }

    [field: SerializeField] public PlayerMovementData playerMovement { get; private set; }
}