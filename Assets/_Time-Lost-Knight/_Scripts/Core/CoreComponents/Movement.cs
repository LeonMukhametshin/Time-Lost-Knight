using System;
using UnityEngine;

public class Movement : CoreComponent, IUpdate, IAcceleration, IEffectable
{
    [field: SerializeField] public Rigidbody2D rb { get; private set; }
    public Vector2 currentVelocity { get; private set; }
    public bool canSetVelocity { get; set; } = true;

    private Vector2 _savedVelocity;
    private float _savedGravity;

    private Vector2 m_workspace;
    private float m_acceleration;

    public void Update() =>
        currentVelocity = rb.linearVelocity;

    public void SetDrag(float linearDamping) =>
        rb.linearDamping = linearDamping;

    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        m_workspace.Set(angle.x * velocity * direction, angle.y * velocity);
        rb.linearVelocity = m_workspace;
        currentVelocity = m_workspace;
    }

    public void SetVelocity(float velocity, Vector2 direction)
    {
        m_workspace = direction * velocity;
        SetFinalVelocity();
    }

    public void SetVelocityZero()
    {
        m_workspace = Vector2.zero;
        SetFinalVelocity();
    }

    public void SetVelocityX(float velocity)
    {
        m_workspace.Set(velocity, rb.linearVelocityY);
        SetFinalVelocity();
    }

    public void SetVelocityXSmooth(float targetVelocityX, float acceleration, float deceleration)
    {
        var rate = Mathf.Abs(targetVelocityX) > 0.01f ? acceleration : deceleration;
        var velocityX = Mathf.Lerp(rigidbody2D.linearVelocityX, targetVelocityX, rate * Time.deltaTime);

        m_workspace.Set(velocityX, rigidbody2D.linearVelocityY);
        SetFinalVelocity();
    }

    public void SetVelocityY(float velocity)
    {
        m_workspace.Set(rb.linearVelocityX, velocity);
        SetFinalVelocity();
    }

    private void SetFinalVelocity()
    {
        if (canSetVelocity)
        {
            rb.linearVelocity = m_workspace;
            currentVelocity = m_workspace;
        }
    }

    public void IncreaseAcceleration(float delta)
    {
        if (delta < 0)
        {
            throw new ArgumentException("Delta can`t be negative", nameof(delta));
        }

        m_acceleration += delta;
        SetSpeed();
    }

    public void DecreaseAcceleration(float delta)
    {
        if (delta < 0)
        {
            throw new ArgumentException("Delta can`t be negative", nameof(delta));
        }

        m_acceleration -= delta;
        SetSpeed();
    }

    private void SetSpeed()
    {
        var acceleration = m_acceleration > 0
            ? m_acceleration
            : 1;

        m_workspace *= acceleration;
    }

    public void SetPaused(bool paused)
    {
        if (paused)
        {
            _savedVelocity = rb.linearVelocity;
            _savedGravity = rb.gravityScale;

            canSetVelocity = false;
            rb.linearVelocity = Vector2.zero;
            rb.gravityScale = 0f;
        }
        else
        {
            rb.gravityScale = _savedGravity;
            canSetVelocity = true;
            rb.linearVelocity = _savedVelocity;
        }
    }
}