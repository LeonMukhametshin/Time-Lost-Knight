using System.Collections;
using UnityEngine;

public class DashAbility : IPlayerAbility
{
    private readonly PlayerMovement m_playerMovement;
    private readonly CoroutineRunner runner;

    public bool isActive => m_isActive;
    public bool canExecute => true;
    public bool isEnabledByDefault => false;
    public string key => "Dash";

    private bool m_isActive = false;

    public DashAbility(PlayerMovement movement, CoroutineRunner runner)
    {
        m_playerMovement = movement;
        this.runner = runner;
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
        m_playerMovement.LastPressedDashTime = m_playerMovement.Data.DashInputBufferTime;
    }

    public void Update()
    {
        if (!isActive) return;

        if (m_playerMovement.LastPressedDashTime > 0)
            TryDash();
    }

    private void TryDash()
    {
        if (!CanDash()) return;
        runner.Run(DashRoutine());
    }

    private IEnumerator DashRoutine()
    {
        StartState();

        Vector2 dir = m_playerMovement.MoveInput != Vector2.zero
            ? m_playerMovement.MoveInput.normalized
            : (m_playerMovement.IsFacingRight ? Vector2.right : Vector2.left);

        float start = Time.time;
        while (Time.time - start < m_playerMovement.Data.DashAttackTime)
        {
            m_playerMovement.m_rigidbody.linearVelocity = dir * m_playerMovement.Data.DashSpeed;
            yield return null;
        }

        EndState();
    }

    private void StartState()
    {
        m_playerMovement.LastPressedDashTime = 0;
        m_playerMovement.DashesLeft--;
        m_playerMovement.IsDashing = true;
        m_playerMovement.IsDashAttacking = true;
    }

    private void EndState()
    {
        m_playerMovement.IsDashAttacking = false;
        m_playerMovement.IsDashing = false;
    }

    private bool CanDash() =>
        m_playerMovement.LastPressedDashTime > 0 &&
        m_playerMovement.DashesLeft > 0 &&
        !m_playerMovement.IsDashing;
}