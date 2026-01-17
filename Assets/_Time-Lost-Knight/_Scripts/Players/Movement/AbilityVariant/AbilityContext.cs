using UnityEngine;

public struct AbilityContext
{
    public AbilityContext(Vector2 moveDirection, Vector2 velocity,
        bool isGrounded, int xScale)
    {
        this.moveDirection = moveDirection;
        this.velocity = velocity;
        this.isGrounded = isGrounded;
        this.xScale = xScale;
    }

    public Vector2 moveDirection;
    public Vector2 velocity;
    public bool isGrounded;
    public int xScale;
}