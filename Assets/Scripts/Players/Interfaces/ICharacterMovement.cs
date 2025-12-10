using UnityEngine;

public interface ICharacterMovement
{
    float MaxHorizontalSpeed { get; }
    float CurrentHorizontalSpeed { get; }
    bool IsJumping { get; }

    void SetHorizontalVelocity(float velocity);
    void Jump(float jumpForce);
    void AddVerticalVelocity(float velocityDelta);
    bool IsGrounded();
    Vector2 GetCurrentVelocity();
    float GetHorizontalVelocity();
    float GetVerticalVelocity();
}
