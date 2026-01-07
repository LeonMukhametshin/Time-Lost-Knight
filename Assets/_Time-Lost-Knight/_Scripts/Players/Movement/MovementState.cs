public class MovementState
{
    public bool isJumping { get; set; }
    public bool isDashing { get; set; }
    public bool isFacingRight { get; set; } = true;

    public int dashesLeft { get; set; }

    public float lastOnGroundTime { get; set; }
    public float lastPressedJumpTime { get; set; }
    public float lastPressedDashTime { get; set; }

    public MovementState(int maxDashes)
    {
        dashesLeft = maxDashes;
    }

    public void Tick(float deltaTime)
    {
        lastOnGroundTime -= deltaTime;
        lastPressedJumpTime -= deltaTime;
        lastPressedDashTime -= deltaTime;
    }

    public void RefreshGrounded(float coyoteTime, int maxDashes)
    {
        lastOnGroundTime = coyoteTime;
        isJumping = false;
        dashesLeft = maxDashes;
    }

    public void Flip()
    {
        isFacingRight = !isFacingRight;
    }
}