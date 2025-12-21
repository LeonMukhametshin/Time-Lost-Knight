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
        m_playerMovement.lastPressedDashTime = m_playerMovement.data.DashInputBufferTime;
    }

    public void Update()
    {
        if (!isActive) return;

        if (m_playerMovement.lastPressedDashTime > 0)
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

        Vector2 dir = m_playerMovement.moveInput != Vector2.zero
            ? m_playerMovement.moveInput.normalized
            : (m_playerMovement.isFacingRight ? Vector2.right : Vector2.left);

        float start = Time.time;
        while (Time.time - start < m_playerMovement.data.DashAttackTime)
        {
            m_playerMovement.rigidbody.linearVelocity = dir * m_playerMovement.data.DashSpeed;
            yield return null;
        }

        EndState();
    }

    private void StartState()
    {
        m_playerMovement.lastPressedDashTime = 0;
        m_playerMovement.dashesLeft--;
        m_playerMovement.isDashing = true;
        m_playerMovement.isDashAttacking = true;
    }

    private void EndState()
    {
        m_playerMovement.isDashAttacking = false;
        m_playerMovement.isDashing = false;
    }

    private bool CanDash() =>
        m_playerMovement.lastPressedDashTime > 0 &&
        m_playerMovement.dashesLeft > 0 &&
        !m_playerMovement.isDashing;
}