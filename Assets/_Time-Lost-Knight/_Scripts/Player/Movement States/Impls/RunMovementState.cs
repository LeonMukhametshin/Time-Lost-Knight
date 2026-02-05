using Inputs;
using UnityEngine;

public class RunMovementState : GroundedMovementState
{
    private readonly Rigidbody2D m_rigidbody;
    private readonly PlayerMoveData m_data;

    protected Vector2 moveDirection;

    public RunMovementState(MovementStateMachine fsm, PlayerInputController inputs, Rigidbody2D rigidbody, PlayerMoveData data, GroundContactChecker checker, 
        MovementAbilityCharges abilityResourceController) 
        : base(fsm, inputs, checker, abilityResourceController)
    {
        m_rigidbody = rigidbody;
        m_data = data;
    }

    public override void FixedUpdate()
    {
        moveDirection = m_input.moveDirection;

        if (moveDirection.sqrMagnitude <= 0.01f)
        {
            fsm.SetState<IdleMovementState>();
        }

        Move(moveDirection);
    }

    protected virtual void Move(Vector2 direction)
    {
        float targetSpeed = direction.x * m_data.runMaxSpeed;
        float currentSpeed = m_rigidbody.linearVelocity.x;

        bool hasInput = Mathf.Abs(direction.x) > 0.01f;

        float accel = hasInput
            ? m_data.runAcceleration
            : m_data.runDeceleration;

        float newSpeed = Mathf.MoveTowards(
            currentSpeed,
            targetSpeed,
            accel
        );

        m_rigidbody.linearVelocity = new Vector2(newSpeed, m_rigidbody.linearVelocity.y);
    }
}