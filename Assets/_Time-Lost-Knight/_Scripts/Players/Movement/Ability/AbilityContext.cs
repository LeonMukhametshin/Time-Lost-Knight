using UnityEngine;

public struct AbilityContext
{
    public AbilityContext(Vector2 moveDirection, Vector2 velocity,
        bool isGrounded, bool canDash, int xScale)
    {
        this.moveDirection = moveDirection;
        this.velocity = velocity;
        this.isGrounded = isGrounded;
        this.canDash = canDash;
        this.xScale = xScale;
    }

    public Vector2 moveDirection;
    public Vector2 velocity;
    public bool isGrounded;
    public bool canDash;
    public int xScale;
}