using Inputs;
using UnityEngine;

public class AirborneMovementState : MovementState
{
    private readonly Rigidbody2D m_rigidbody;
    private readonly PlayerInputController m_inputs;

    private const float ariMoveSpeed = 7f;
    private const float airAcceleration = 0.5f;

    public AirborneMovementState(MovementStateMachine fsm, Rigidbody2D rigidbody, PlayerInputController inputs) : base(fsm)
    {
        m_rigidbody = rigidbody;
        m_inputs = inputs;
    }

    public override void FixedUpdate()
    {
        Move();
    }

    public virtual void Move()
    {
        float targetSpeed = m_inputs.moveDirection.x * ariMoveSpeed;
        float newSpeed = Mathf.MoveTowards(m_rigidbody.linearVelocity.x, targetSpeed, airAcceleration);
        m_rigidbody.linearVelocity = new Vector2(newSpeed, m_rigidbody.linearVelocity.y);
    }
}