using System;
using UnityEngine;

public class GroundContactChecker : MonoBehaviour
{
    public event Action groundedStateChanged;

    private BoxCollider2D m_collider;

    private LayerMask m_groundLayer;
    private float m_groundCheckDistance;

    public bool isGround
    {
        get => m_grounded;
        set
        {
            if(isGround != value)
            {
                m_grounded = value;
                groundedStateChanged?.Invoke();
            }
        }
    }

    private bool m_grounded;

    private bool m_isInitialize;

    public void Initialize(BoxCollider2D collider, GroundCheckData data)
    {
        if(m_isInitialize)
        {
            return;
        }

        m_collider = collider;

        m_groundLayer = data.groundLayer;
        m_groundCheckDistance = data.groundCheckDistance;

        m_isInitialize = true;
    }

    private void Update()
    {
        if (!m_isInitialize)
        {
            return;
        }

        IsGrounded();
    }

    public void IsGrounded()
    {
        Bounds bounds = m_collider.bounds;

        Vector2 size = new Vector2
        (
            bounds.size.x,
            bounds.size.y
        );

        isGround = Physics2D.BoxCast(
            bounds.center,
            size,
            0f,
            Vector2.down,
            m_groundCheckDistance,
            m_groundLayer
        );
    }
}