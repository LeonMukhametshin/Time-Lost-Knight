using Inputs;
using System;
using System.Collections;
using UnityEngine;

public class JumpMovementState : AirborneMovementState
{
    public event Action jumpFineshed;

    private readonly PlayerJumpData m_jumpData;
    private readonly Rigidbody2D m_rigidbody;
    private readonly CoroutineRunner m_coroutines;

    public JumpMovementState(MovementStateMachine fsm, PlayerInputController inputs, 
        Rigidbody2D rigidbody, PlayerJumpData data, 
        CoroutineRunner coroutine) 
        : base(fsm, rigidbody, inputs)
    {
        m_jumpData = data;
        m_coroutines = coroutine;
        m_rigidbody = rigidbody;
    }

    public override void Enter()
    {
        m_rigidbody.linearVelocity = new Vector2(
            m_rigidbody.linearVelocity.x,
            0f);

        m_coroutines.StartCoroutine(JumpRoutine());
    }

    public override void Exit()
    {
        m_coroutines.StopCoroutine(JumpRoutine());
    }

    private IEnumerator JumpRoutine()
    {
        float elapsed = 0f;

        while (elapsed < m_jumpData.jumpDuration)
        {
            float progress = elapsed / m_jumpData.jumpDuration;

            float curveValue = m_jumpData.jumpCurve.Evaluate(progress);
            float verticalVelocity = curveValue * m_jumpData.jumpHeight;

            Vector2 velocity = m_rigidbody.linearVelocity;
            velocity.y = verticalVelocity;
            m_rigidbody.linearVelocity = velocity;

            elapsed += Time.deltaTime;

            yield return null;
        }

        jumpFineshed?.Invoke();
    }
}