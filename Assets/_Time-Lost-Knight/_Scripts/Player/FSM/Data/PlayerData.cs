using NUnit.Framework;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/Player/Player_Data")]
public class PlayerData : ScriptableObject
{
    [field: SerializeField] public float movementSpeed { get; private set; }   

    [Header("Jump")]
    [field: SerializeField] public float jumpVelocity { get; private set; } = 5f;
    [field: SerializeField] public int amountOfJumps { get; private set; }

    [Header("Wall Jump State")]
    [field: SerializeField] public float wallJumpVelocity { get; private set; }
    [field: SerializeField] public float wallJumpTime { get; private set; }
    [field: SerializeField] public Vector2 wallJumpAnge { get; private set; }

    [Header("Air")]
    [field: SerializeField] public float coyoteTime { get; private set; }
    [field: SerializeField] public float jumpHeightMultiplier { get; private set; }

    [Header("Wall Slide")]
    [field: SerializeField] public float wallSlideVelocity { get; private set; }
    [field: SerializeField] public float wallClimbVelocity { get; private set; }

    [Header("Ledge Climb")]
    [field: SerializeField] public Vector2 startOffset { get; private set; }
    [field: SerializeField] public Vector2 stopOffset { get; private set; }

    [Header("Dash state")]
    [field: SerializeField] public float dashCooldown { get; private set; }
    [field: SerializeField] public float maxHoldTime { get; private set; }
    [field: SerializeField] public float holdTimeScale { get; private set; }
    [field: SerializeField] public float dashTime { get; private set; }
    [field: SerializeField] public float dashVelocity { get; private set; }
    [field: SerializeField] public float drag { get; private set; }
    [field: SerializeField] public float dashEndYMultiplier { get; private set; }
    [field: SerializeField] public float distanceBetweenAfterImages { get; private set; }

    [Header("Crouch")]
    [field: SerializeField] public float crouchMovementVelocity { get; private set; }
    [field: SerializeField] public float crouchColliderHeight { get; private set; }
    [field: SerializeField] public float standColliderHeight { get; private set; }

    [Header("Check")]
    [field: SerializeField] public float groundCheckRadius { get; private set; }
    [field: SerializeField] public float ceilingCheckRadius { get; private set; }
    [field: SerializeField] public float wallCheckDistance { get; private set; }
    [field: SerializeField] public LayerMask groundLayer { get; private set; }
}