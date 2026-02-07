using System;
using System.Collections;
using UnityEngine;

public class DashMovementState : MovementState
{
    public event Action dashFinished;

    private readonly DashData m_data;
    private readonly Rigidbody2D m_rigidbody;
    private readonly Transform m_transform;
    private readonly CoroutineRunner m_coroutines;

    private Coroutine m_dashCoroutine;

    public DashMovementState(
        MovementStateMachine fsm,
        Rigidbody2D rigidbody,
        Transform transform,
        DashData data,
        CoroutineRunner coroutine)
        : base(fsm)
    {
        m_data = data;
        m_rigidbody = rigidbody;
        m_transform = transform;
        m_coroutines = coroutine;
    }

    public override void Enter()
    {
        m_dashCoroutine = m_coroutines.StartCoroutine(DashRoutine());
    }

    public override void Exit()
    {
        if (m_dashCoroutine != null)
            m_coroutines.StopCoroutine(m_dashCoroutine);
    }

    private IEnumerator DashRoutine()
    {
        float originalGravity = m_rigidbody.gravityScale;
        m_rigidbody.gravityScale = 0f;

        float timer = 0f;
        float direction = Mathf.Sign(m_transform.localScale.x);

        while (timer < m_data.duration)
        {
            m_rigidbody.linearVelocity = new Vector2(
                direction * m_data.dashSpeed, 0f);

            timer += Time.deltaTime;
            yield return null;
        }

        m_rigidbody.gravityScale = originalGravity;
        dashFinished?.Invoke();
    }
}