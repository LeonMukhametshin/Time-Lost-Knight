using Inputs;
using UnityEngine;

public class IdleMovementState : GroundedMovementState
{
    private readonly Rigidbody2D m_rigidbody;

    public IdleMovementState(MovementStateMachine fsm, PlayerInputController input, Rigidbody2D rigidbody, GroundContactChecker checker, 
        MovementAbilityCharges abilityResourceController)
        : base(fsm, input, checker, abilityResourceController)
    {
        m_rigidbody = rigidbody;
    }

    public override void Enter()
    {
        m_rigidbody.linearVelocity = new Vector2(0f, m_rigidbody.linearVelocity.y);
    }

    public override void FixedUpdate()
    {
        if(m_input.moveDirection.sqrMagnitude != 0f)
        {
            fsm.SetState<RunMovementState>();
        }
    }
}   