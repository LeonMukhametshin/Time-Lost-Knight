using System.Collections;
using UnityEngine;

public class DashAbility : IPlayerAbility
{
    public string key => "Dash";
    public bool isActive => m_isActive;
    public bool isEnabledByDefault => true;

    private bool m_isActive;

    private readonly PlayerMovementController m_movement;
    private readonly PlayerData m_data;
    private readonly CoroutineRunner m_runner;

    private Vector2 m_moveInput;

    public DashAbility(
        PlayerMovementController movement,
        CoroutineRunner runner,
        PlayerData data)
    {
        m_movement = movement;
        m_runner = runner;
        m_data = data;
    }

    public void Activate()
    {
        m_isActive = true;
    }

    public void Deactivate()
    {
        m_isActive = false;
    }

    public void DoDash()
    {
        m_movement.state.LastPressedDashTime = m_data.DashInputBufferTime;
    }

    public void SetMoveInput(Vector2 input)
    {
        m_moveInput = input;
    }

    public void Update()
    {
        if (!m_isActive) return;

        if (m_movement.state.LastPressedDashTime > 0)
        {
            TryDash();
        }
    }

    private void TryDash()
    {
        if (!CanDash()) return;

        m_runner.Run(DashRoutine());
    }

    private IEnumerator DashRoutine()
    {
        StartState();

        Vector2 dir = m_moveInput != Vector2.zero
            ? m_moveInput.normalized
            : (m_movement.state.IsFacingRight ? Vector2.right : Vector2.left);

        float startTime = Time.time;

        while (Time.time - startTime < m_data.DashAttackTime)
        {
            m_movement.rigidbody.linearVelocity = dir * m_data.DashSpeed;
            yield return null;
        }

        EndState();
    }

    private void StartState()
    {
        var state = m_movement.state;

        state.LastPressedDashTime = 0;
        state.DashesLeft--;
        state.IsDashing = true;
    }

    private void EndState()
    {
        var state = m_movement.state;
        state.IsDashing = false;
    }

    private bool CanDash()
    {
        var state = m_movement.state;

        return state.LastPressedDashTime > 0 &&
               state.DashesLeft > 0 &&
               !state.IsDashing;
    }
}