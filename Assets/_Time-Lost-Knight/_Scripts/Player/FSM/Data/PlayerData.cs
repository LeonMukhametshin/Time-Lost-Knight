using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/Player/Player_Data")]
public class PlayerData : ScriptableObject
{
    [field: SerializeField][Min(0)] public float movementSpeed { get; private set; }   

    [Header("Jump")]
    [field: SerializeField][Min(0)] public float jumpVelocity { get; private set; } = 5f;
    [field: SerializeField][Min(1)] public int amountOfJumps { get; private set; }

    [Header("Wall Jump State")]
    [field: SerializeField][Min(0)] public float wallJumpVelocity { get; private set; }
    [field: SerializeField][Min(0)] public float wallJumpTime { get; private set; }
    [field: SerializeField][Min(0)] public Vector2 wallJumpAnge { get; private set; }

    [Header("Air")]
    [field: SerializeField][Min(0)] public float coyoteTime { get; private set; }
    [field: SerializeField][Min(0)] public float jumpHeightMultiplier { get; private set; }

    [Header("Wall Slide")]
    [field: SerializeField][Min(0)] public float wallSlideVelocity { get; private set; }
    [field: SerializeField][Min(0)] public float wallClimbVelocity { get; private set; }

    [Header("Ledge Climb")]
    [field: SerializeField][Min(0)] public Vector2 startOffset { get; private set; }
    [field: SerializeField][Min(0)] public Vector2 stopOffset { get; private set; }

    [Header("Dash state")]
    [field: SerializeField][Min(0)] public float dashCooldown { get; private set; }
    [field: SerializeField][Min(0)] public float maxHoldTime { get; private set; }
    [field: SerializeField][Min(0)] public float holdTimeScale { get; private set; }
    [field: SerializeField][Min(0)] public float dashTime { get; private set; }
    [field: SerializeField][Min(0)] public float dashVelocity { get; private set; }
    [field: SerializeField][Min(0)] public float drag { get; private set; }
    [field: SerializeField][Min(0)] public float dashEndYMultiplier { get; private set; }
    [field: SerializeField][Min(0)] public float distanceBetweenAfterImages { get; private set; }

    [Header("Crouch")]
    [field: SerializeField][Min(0)] public float crouchMovementVelocity { get; private set; }
    [field: SerializeField][Min(0)] public float crouchColliderHeight { get; private set; }
    [field: SerializeField][Min(0)] public float standColliderHeight { get; private set; }

    [Header("One Way Platform")]
    [field: SerializeField] public int oneWayPlatformLayer { get; private set; }
    [field: SerializeField][field: Min(0)] public float dropThroughDuration { get; private set; } = 0.25f;
    [field: SerializeField][field: Min(0)] public float dropVelocity { get; private set; } = 3f;
}