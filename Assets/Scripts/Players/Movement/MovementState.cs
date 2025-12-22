using UnityEngine;

public class MovementState
{
    public bool isJumping { get; set; }
    public bool IsDashing { get; set; }
    public bool IsFacingRight { get; set; } = true;

    public int DashesLeft { get; set; }

    public float lastOnGroundTime { get; set; }
    public float lastPressedJumpTime { get; set; }
    public float LastPressedDashTime { get; set; }

    public MovementState(int maxDashes)
    {
        DashesLeft = maxDashes;
    }

    public void Tick(float deltaTime)
    {
        lastOnGroundTime -= deltaTime;
        lastPressedJumpTime -= deltaTime;
        LastPressedDashTime -= deltaTime;
    }

    public void RefreshGrounded(float coyoteTime, int maxDashes)
    {
        lastOnGroundTime = coyoteTime;
        isJumping = false;
        DashesLeft = maxDashes;
    }

    public void Flip()
    {
        IsFacingRight = !IsFacingRight;
    }
}