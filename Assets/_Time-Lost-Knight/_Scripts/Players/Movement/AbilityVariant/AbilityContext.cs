using UnityEngine;

public struct AbilityContext
{
    public AbilityContext(Vector2 moveDirection, int xScale, bool grounded)
    {
        this.moveDirection = moveDirection;
        this.xScale = xScale;
        this.grounded = grounded;
    }

    public Vector2 moveDirection { get; set; }
    public int xScale { get; set; }
    public bool grounded { get; set; }
}