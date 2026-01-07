using UnityEngine;

public class GroundChecker
{
    private readonly BoxCollider2D m_collider;
    private readonly PlayerData m_data;

    public GroundChecker(BoxCollider2D collider, PlayerData data)
    {
        m_collider = collider;
        m_data = data;
    }

    public bool IsGrounded()
    {
        Bounds bounds = m_collider.bounds;
        float skinWidth = 0.02f;

        Vector2 size = new(
            bounds.size.x - skinWidth,
            bounds.size.y
        );

        return Physics2D.BoxCast(
            bounds.center,
            size,
            0f,
            Vector2.down,
            m_data.GroundCheckDistance,
            m_data.GroundLayer
        );
    }
}