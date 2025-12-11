using UnityEngine;

public interface ICharacterMovement
{
    float Gravity { get; set; }
    float MaxHorizontalSpeed { get; }
    float CurrentHorizontalSpeed { get; }
    bool IsJumping { get; }
    bool IsDashing { get; }

    void SetHorizontalVelocity(float velocity);
    void Jump(float jumpForce);
    void Dash(Vector2 dashDirection);
    void AddVerticalVelocity(float velocityDelta);
    bool IsGrounded();
    Vector2 GetCurrentVelocity();
    float GetHorizontalVelocity();
    float GetVerticalVelocity();
    void SetGravity(float newGravity);
}
