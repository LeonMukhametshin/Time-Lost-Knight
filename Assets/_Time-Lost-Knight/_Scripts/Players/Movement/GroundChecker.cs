using UnityEditor;
using UnityEngine;

public class GroundChecker
{
    private readonly BoxCollider2D m_collider;
    private readonly PlayerGroundCheckData m_data;

    public GroundChecker(BoxCollider2D collider, PlayerGroundCheckData data)
    {
        m_collider = collider;
        m_data = data;
    }

    public bool IsGrounded()
    {
        Bounds bounds = m_collider.bounds;

        Vector2 size = new Vector2
        (
            bounds.size.x,
            bounds.size.y
        );

        return Physics2D.BoxCast(
            bounds.center,
            size,
            0f,
            Vector2.down,
            m_data.groundCheckDistance,
            m_data.groundLayer
        );
    }
}