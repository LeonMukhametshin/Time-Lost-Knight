using Inputs;
using UnityEngine;

public class FallMovementState : AirborneMovementState
{
    private Rigidbody2D m_rigidbody;
    private FallData m_data;

    private float m_timer;

    public FallMovementState(MovementStateMachine fsm, PlayerInputController inputs,
        Rigidbody2D rigidbody, FallData data)
        : base(fsm, rigidbody, inputs)
    {
        m_rigidbody = rigidbody;
        m_data = data;
    }

    public override void FixedUpdate()
    {
        HandleFallAcceleration();
    }

    public override void Exit()
    {
        m_timer = 0f;
    }

    private void HandleFallAcceleration()
    {
        float duration = Mathf.Max(m_data.fallAccelerationDuration, 0.0001f);
        float progress = Mathf.Clamp01(m_timer / duration);
        float curveValue = m_data.fallAccelerationCurve.Evaluate(progress);
        float acceleration = m_data.fallAcceleration * curveValue;

        Vector2 velocity = m_rigidbody.linearVelocity;
        velocity.y = Mathf.Max(velocity.y - (acceleration * Time.deltaTime), -m_data.maxFallSpeed);
        m_rigidbody.linearVelocity = velocity;

        m_timer += Time.deltaTime;
    }
}