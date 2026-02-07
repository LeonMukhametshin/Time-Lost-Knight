using Inputs;
using System;
using System.Collections;
using UnityEngine;

public class JumpMovementState : AirborneMovementState
{
    public event Action jumpFineshed;

    private readonly JumpData m_jumpData;
    private readonly Rigidbody2D m_rigidbody;
    private readonly CoroutineRunner m_coroutines;

    private Coroutine m_jumpCoroutine;

    public JumpMovementState(
        MovementStateMachine fsm,
        PlayerInputController inputs,
        Rigidbody2D rigidbody,
        JumpData data,
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

        m_jumpCoroutine = m_coroutines.StartCoroutine(JumpRoutine());
    }

    public override void Exit()
    {
        if (m_jumpCoroutine != null)
            m_coroutines.StopCoroutine(m_jumpCoroutine);
    }

    private IEnumerator JumpRoutine()
    {
        float elapsed = 0f;

        while (elapsed < m_jumpData.jumpDuration)
        {
            float progress = elapsed / m_jumpData.jumpDuration;
            float curveValue = m_jumpData.jumpCurve.Evaluate(progress);

            m_rigidbody.linearVelocity = new Vector2(
                m_rigidbody.linearVelocity.x,
                curveValue * m_jumpData.jumpHeight
            );

            elapsed += Time.deltaTime;
            yield return null;
        }

        jumpFineshed?.Invoke();
    }
}